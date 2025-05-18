using UnityEngine;

public class KillScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched kill platform");

            // Destroy the player GameObject
            Destroy(other.gameObject);

            // Call the Game Over UI from your GameManager
            GameManager.instance?.ShowGameOver();

            // Optional: destroy remaining snowballs if needed
            GameObject[] snowballs = GameObject.FindGameObjectsWithTag("Snowball");
            foreach (GameObject snowball in snowballs)
            {
                Destroy(snowball);
            }
        }
    }
}
