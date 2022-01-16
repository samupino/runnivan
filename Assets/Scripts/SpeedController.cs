using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float initialSpeed;
    public float constantIncrement;
    public float currentSpeed { get; private set; }

    bool active;

    void Start()
    {
        currentSpeed = initialSpeed;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
        currentSpeed += constantIncrement * Time.deltaTime;
        }
    }

    public void Stop()
    {
        active = false;
        currentSpeed = 0;
    }
}
