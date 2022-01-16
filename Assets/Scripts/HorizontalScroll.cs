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
        float newX = transform.position.x - speedController.currentSpeed * Time.deltaTime * speedMultiplier;

        if (newX <= deathX)
        {
            newX += spawnX - deathX;
        }

        transform.position = new Vector3(
            newX,
            transform.position.y,
            transform.position.z
        );
    }
}
