using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f; // The distance to move in one direction from the starting point

    private Vector2 startPosition;
    private float traveledDistance = 0f; // Track the distance traveled from the starting point
    private int direction = 1; // Use 1 for right, -1 for left

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the platform
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // Update the traveled distance
        traveledDistance += moveSpeed * Time.deltaTime;

        // Check if the platform needs to change direction
        if (traveledDistance >= moveDistance)
        {
            direction *= -1; // Change direction
            traveledDistance = 0f; // Reset the traveled distance
            startPosition = transform.position; // Update the start position
        }
    }
}
