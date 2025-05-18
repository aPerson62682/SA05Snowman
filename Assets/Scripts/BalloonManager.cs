using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonManager : MonoBehaviour
{
    public static BalloonManager instance;

    public GameObject balloonPrefab;
    public GameObject blackBalloonPrefab; // Add this
    private int spawnCounter = 0;


    public List<Transform> topSpawnPoints;
    public List<Transform> bottomSpawnPoints;
    public List<Transform> leftSpawnPoints;
    public List<Transform> rightSpawnPoints;

    private Dictionary<string, GameObject> activeBalloons = new Dictionary<string, GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SpawnBalloonOnSide("Top");
        SpawnBalloonOnSide("Bottom");
        SpawnBalloonOnSide("Left");
        SpawnBalloonOnSide("Right");
    }

    public void RemoveBalloon(string side)
    {
        if (activeBalloons.ContainsKey(side))
        {
            Destroy(activeBalloons[side]);
            activeBalloons.Remove(side);
            StartCoroutine(RespawnBalloonAfterDelay(3f, side));
        }
    }



    private IEnumerator RespawnBalloonAfterDelay(float delay, string side)
    {
        yield return new WaitForSeconds(delay);
        SpawnBalloonOnSide(side);
    }

    private void SpawnBalloonOnSide(string side)
    {
        if (activeBalloons.ContainsKey(side))
            return;

        List<Transform> spawnList = GetSpawnList(side);
        if (spawnList == null || spawnList.Count == 0) return;

        Transform spawnPoint = spawnList[Random.Range(0, spawnList.Count)];

        spawnCounter++;
        bool spawnBlack = (spawnCounter % 6 == 0); // Every 6th balloon is black
        GameObject prefabToUse = spawnBlack ? blackBalloonPrefab : balloonPrefab;


        GameObject newBalloon = Instantiate(prefabToUse, spawnPoint.position, Quaternion.identity);
        newBalloon.tag = "Balloons";

        var collectScript = newBalloon.GetComponent<BalloonCollect>();
        if (collectScript != null)
        {
            collectScript.balloonSide = side;
            collectScript.isBlack = spawnBlack;
        }

        activeBalloons[side] = newBalloon;
    }

    private List<Transform> GetSpawnList(string side)
    {
        switch (side)
        {
            case "Top": return topSpawnPoints;
            case "Bottom": return bottomSpawnPoints;
            case "Left": return leftSpawnPoints;
            case "Right": return rightSpawnPoints;
            default: return null;
        }
    }
}