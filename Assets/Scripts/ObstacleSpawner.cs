using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    public SpeedManager speedManager;
    public float minimumSpawnDelay;
    public float maximumSpawnDelay;
    public GameObject[] obstacles;

    float nextObstacleTimer;

    // Start is called before the first frame update
    void Start()
    {
        nextObstacleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nextObstacleTimer -= Time.deltaTime;

        if (nextObstacleTimer <= 0)
        {
            SpawnRandom();
            nextObstacleTimer = Random.Range(minimumSpawnDelay, maximumSpawnDelay);
        }
    }

    void SpawnRandom()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(
            obstacles[randomIndex],
            transform.position,
            Quaternion.identity
        );
        Rigidbody2D obstacleRB = obstacle.GetComponent<Rigidbody2D>();
        Vector3 newVelocity = obstacleRB.velocity;
        newVelocity.x = -speedManager.currentSpeed;
        obstacleRB.velocity = newVelocity;
    }
}
