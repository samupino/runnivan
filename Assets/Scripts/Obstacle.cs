using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    public SpeedController speedController;

    public float speedMultiplier;
    public float deathXDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        float positionDelta = speedController.currentSpeed * Time.deltaTime * speedMultiplier;
        Vector3 newPosition = transform.position + Vector3.left * positionDelta;
        transform.position = newPosition;

        if (transform.position.x < deathXDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
