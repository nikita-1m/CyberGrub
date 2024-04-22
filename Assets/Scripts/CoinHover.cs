using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHover : MonoBehaviour
{
    // Configuration variables
    public float hoverDistance = 0.1f; // The maximum distance the coin will hover
    public float hoverSpeed = 1f; // The speed of the hovering motion

    // Variables to keep track of the original position and time
    private Vector3 originalPosition;
    private float startTime;

    void Awake()
    {
        // Store the original position and start time
        originalPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = originalPosition.y + Mathf.Sin((Time.time - startTime) * hoverSpeed) * hoverDistance;

        // Update the transform position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
