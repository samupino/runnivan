using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HorizontalScroll : MonoBehaviour
{
    BoxCollider2D _collider;
    float initialPositionX;

    public SpeedController speedController;
    public float speedMultiplier;
    public float deathX;
    public float spawnX;

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        initialPositionX = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 positionDelta = Vector3.left * speedController.currentSpeed * Time.deltaTime * speedMultiplier;
        Vector3 newPosition = transform.position + positionDelta;

        if (newPosition.x <= deathX)
        {
            newPosition.x += spawnX - deathX;
        }

        transform.position = newPosition;
    }
}
