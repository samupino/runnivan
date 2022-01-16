using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [HideInInspector]
    public SpeedController speedController;
    
    public float spawnRadiusY;

    public Obstacle[] obstacles;

    public void SpawnRandom()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        float yOffset = Random.Range(-spawnRadiusY, spawnRadiusY);

        Obstacle obstacle = Instantiate(
            obstacles[randomIndex],
            transform.position + Vector3.up * yOffset,
            Quaternion.identity
        );

        obstacle.speedController = speedController;
    }
}
