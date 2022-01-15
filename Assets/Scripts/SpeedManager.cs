using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float initialSpeed;
    public float constantIncrement;
    public float currentSpeed { get; private set; }

    void Start()
    {
        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += constantIncrement * Time.deltaTime;
    }
}
