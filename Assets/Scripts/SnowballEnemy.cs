using UnityEngine;
using System.Collections;

public class SnowballEnemy : MonoBehaviour
{
    public Transform player;
    private float moveSpeed = 1.5f;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 originalPosition; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position; 
        StartCoroutine(DespawnAndRespawn());
        player = GameObject.FindWithTag("Player").transform;

    }

    void Update()
    {
        if (player == null) return; // Stops if player is destroyed

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = Quaternion.Euler(0, 0, angle);
        direction.Normalize();
        movement = direction;
    }

    void FixedUpdate()
    {
        moveEnemy(movement);
    }

    void moveEnemy(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    IEnumerator DespawnAndRespawn()
    {
        yield return new WaitForSeconds(10f); 
        Vector3 spawnPos = originalPosition; // Store the original position before destroying
        Destroy(gameObject); 
        SnowballSpawner.RespawnEnemy(spawnPos, transform.rotation); // Call the spawner to create a new enemy
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject); // Destroy Player

            GameObject[] snowballs = GameObject.FindGameObjectsWithTag("Snowball");
            foreach (GameObject snowball in snowballs)
            {
                Destroy(snowball); // Destroy All Snowballs
            }
        }
    }
}
