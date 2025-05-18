using UnityEngine;
using System.Collections;

public class SnowballEnemy : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 originalPosition;
    private bool hasRespawned = false;
    public GameObject gameOverScreen; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;

        // Ensure z position is locked to 0
        Vector3 fixedPos = transform.position;
        fixedPos.z = 0f;
        transform.position = fixedPos;

        StartCoroutine(TrackAndLaunch());
        StartCoroutine(SelfDestructAfterTime());

    }

    void FixedUpdate()
    {
        moveEnemy(movement);
    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    IEnumerator TrackAndLaunch()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = transform.position;

        float trackTime = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < trackTime)
        {
            if (player != null)
            {
                targetPosition = player.transform.position;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set movement direction toward where the player was
        movement = (targetPosition - transform.position).normalized;

        // Rotate to face the movement direction
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator SelfDestructAfterTime()
    {
        yield return new WaitForSeconds(10f);
        if (!hasRespawned)
        {
            hasRespawned = true;
            SnowballSpawner.RespawnEnemy(originalPosition, transform.rotation);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (hasRespawned) return;

        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the player");
            hasRespawned = true;
            Destroy(other.gameObject);


            GameManager.instance?.ShowGameOver();



            GameObject[] snowballs = GameObject.FindGameObjectsWithTag("Snowball");
            foreach (GameObject snowball in snowballs)
            {
                Destroy(snowball);
            }

            SnowballSpawner.RespawnEnemy(originalPosition, transform.rotation);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall Hit!");
            hasRespawned = true;
            SnowballSpawner.RespawnEnemy(originalPosition, transform.rotation);
            Destroy(gameObject);
        }
    }
}
