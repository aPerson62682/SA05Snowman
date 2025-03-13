using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private GameObject Purly;

    private Vector3 lastValidPosition;
    private Vector3 startingPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = Purly.GetComponent<Rigidbody>();
        lastValidPosition = Purly.transform.position;
        startingPosition = Purly.transform.position;
    }

    void Update()
    {
        float translation = speed * Time.deltaTime;
        float rotation = rotationSpeed * Time.deltaTime;

        Vector3 newPosition = Purly.transform.position;

        // Movement with transform.position 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            newPosition += new Vector3(-translation, 0, 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            newPosition += new Vector3(translation, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            newPosition += new Vector3(0, translation, 0);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            newPosition += new Vector3(0, -translation, 0);

        Purly.transform.position = newPosition;

        // Rotation using transform.Rotate 
        if (Input.GetKey(KeyCode.Space))
        {
            Purly.transform.Rotate(new Vector3(0, rotation, 0));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Purly.transform.position = lastValidPosition;  // Stop at walls
        }
        else if (collision.gameObject.CompareTag("Kill"))
        {
            Purly.transform.position = startingPosition;  // Brings Purly back to starting position if it hits the kill plane
        }
        else if (collision.gameObject.CompareTag("Balloons"))
        {
            Destroy(collision.gameObject);
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            lastValidPosition = Purly.transform.position;
        }
    }
}