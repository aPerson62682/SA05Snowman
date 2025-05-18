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
        if (instance != null)
        {
            instance.StartCoroutine(instance.SpawnEnemy(position, rotation));
        }
    }

    IEnumerator SpawnEnemy(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(5f);

        GameObject newEnemy = Instantiate(enemyPrefab, position, rotation);
        newEnemy.tag = targetTag;

        SnowballEnemy enemyScript = newEnemy.GetComponent<SnowballEnemy>();
        if (enemyScript != null)
        {
            GameObject screen = GameObject.Find("GameOverScreen");
            Debug.Log("Trying to find GameOverScreen... Found? " + (screen != null));

             screen = GameObject.Find("GameOverScreen");
            if (screen != null)
            {
                enemyScript.gameOverScreen = screen;
            }
        }
    }

}
