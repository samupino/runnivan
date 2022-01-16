using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [HideInInspector]
    public SpeedManager speedManager;

    public float deathXDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + Vector3.left * speedManager.currentSpeed * Time.deltaTime;
        transform.position = newPosition;

        if (transform.position.x < deathXDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
