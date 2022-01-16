using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float deathXDistance;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < deathXDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
