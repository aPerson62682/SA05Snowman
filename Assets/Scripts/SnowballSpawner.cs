using UnityEngine;
using System.Collections;

public class SnowballSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public string targetTag = "Snowball";
    public static SnowballSpawner instance;

    void Awake()
    {
        instance = this; // Assign this instance so enemies can call it
    }
    public static void RespawnEnemy(Vector3 position, Quaternion rotation)
    {
        instance.StartCoroutine(instance.SpawnEnemy(position, rotation));
    }
    IEnumerator SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(20f); // Wait 20 seconds before respawning
        GameObject newEnemy = Instantiate(enemyPrefab, position, rotation); // Respawn enemy at original location
        newEnemy.tag = targetTag; // Set the tag of the new enemy
    }
}
