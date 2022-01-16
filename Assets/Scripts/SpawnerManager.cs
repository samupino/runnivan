using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    public SpeedManager speedManager;

    public float minSpawnDelay;
    public float maxSpawnDelay;

    public SpawnerWithWeight[] spawners;
    float[] weights;

    float nextObstacleTimer;

    // Start is called before the first frame update
    void Start()
    {
        nextObstacleTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
        weights = new float[spawners.Length];
        for (int i = 0; i< spawners.Length; i++)
        {
            weights[i] = spawners[i].weight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        nextObstacleTimer -= Time.deltaTime;

        if (nextObstacleTimer <= 0)
        {
            spawners[Utils.GetRandomWeightedIndex(weights)].spawner.SpawnRandom();
            nextObstacleTimer = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
}

[System.Serializable]
public class SpawnerWithWeight
{
    public ObstacleSpawner spawner;
    public float weight;
}
