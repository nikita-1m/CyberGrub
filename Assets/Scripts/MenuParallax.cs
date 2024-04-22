using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParallax : MonoBehaviour
{
    public float horizontalSpeed = 0.5f; // Speed at which the background moves horizontally
    public float verticalSpeed = 0f;  // Speed at which the background moves vertically (set to 0 for no vertical movement)
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position; // Remember the start position of the background
    }

    void Update()
    {
        // Calculate the new position for the background
        float newPositionX = Mathf.Repeat(Time.time * horizontalSpeed, 20); // '20' controls the length before it repeats
        float newPositionY = Mathf.Repeat(Time.time * verticalSpeed, 10); // '10' controls the vertical length before it repeats

        // Apply the new position, subtracting for left to right movement
        transform.position = new Vector2(startPos.x - newPositionX, startPos.y + newPositionY);
    }
}