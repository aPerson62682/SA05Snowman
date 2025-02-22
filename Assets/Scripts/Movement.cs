using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private GameObject Purly;

    void Start()
    {
      
    }

    void Update()
    {
        float translation = speed * Time.deltaTime;
        float rotation = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            Purly.transform.position += new Vector3(-translation, 0, 0);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            Purly.transform.position += new Vector3(translation, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            Purly.transform.position += new Vector3(0, translation, 0);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            Purly.transform.position += new Vector3(0, -translation, 0);


        if (Input.GetKey(KeyCode.Space))
            Purly.transform.Rotate(new Vector2(0, rotation));
    }
}
