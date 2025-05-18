using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f; // 🔽 Reduced from 16f
    private bool isFacingRight = true;

    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private bool wasGroundedLastFrame = true;

    [SerializeField] private GameObject landingEffectPrefab;
    [SerializeField] private Transform effectSpawnPoint; // Where the effect appears (usually near the feet)

    public ParticleSystem water;

    // Reference to AudioManager
    private AudioManager audioManager;

    void Start()
    {
        // Initialize AudioManager instance
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayMusic("MainTheme");
            audioManager.musicSource.loop = true;
        }
        else
        {
            Debug.LogError("AudioManager instance not found in the scene.");
        }
    }

    void Update()
    {
        // Get horizontal input (A/D or Left/Right arrows)
        horizontal = Input.GetAxisRaw("Horizontal");

        bool isCurrentlyGrounded = IsGrounded();

        // Landed this frame (was in air, now grounded)
        if (!wasGroundedLastFrame && isCurrentlyGrounded)
        {
            TriggerLandingEffect();
        }

        wasGroundedLastFrame = isCurrentlyGrounded;

        if (Input.GetKeyDown(KeyCode.W) && isCurrentlyGrounded)
        {
            water.Play();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpingPower, 0f);
            if (audioManager != null)
            {
                audioManager.PlaySFX("JumpSound");
            }
        }

        // Variable jump height (release W)
        if (Input.GetKeyUp(KeyCode.W) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f, 0f);
        }

        // 🔄 Rotate while holding Space
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector3(horizontal * speed, rb.linearVelocity.y, 0f);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;


            if (IsGrounded())
            {
            water.Play();   
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
    private void TriggerLandingEffect()
    {
        Debug.Log("Purly landed!");

        if (landingEffectPrefab != null && effectSpawnPoint != null)
        {
            Instantiate(landingEffectPrefab, effectSpawnPoint.position, Quaternion.identity);
        }

        if (audioManager != null)
        {
            audioManager.PlaySFX("LandSound");
        }
    }

}
