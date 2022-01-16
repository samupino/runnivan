using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public SpeedManager speedManager;
    
    public float spawnRadiusY;

    public GameObject[] obstacles;

    public void SpawnRandom()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        float yOffset = Random.Range(-spawnRadiusY, spawnRadiusY);

        GameObject obstacle = Instantiate(
            obstacles[randomIndex],
            transform.position + Vector3.up * yOffset,
            Quaternion.identity
        );

        Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();
        Vector3 newVelocity = obstacleRB.velocity;
        newVelocity.x = -speedManager.currentSpeed;
        obstacleRB.velocity = newVelocity;
    }
}
