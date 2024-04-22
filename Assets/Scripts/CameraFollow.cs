using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset; // Typically, for 2D, Z offset is set to -10 if the camera is orthographic.

    private float maxVerticalAdjust = 0.5f; // The maximum amount to move the camera down.
    private float playerMaxY = 5.0f; // The Y position of the player at which the maximum offset is applied.

    void LateUpdate()
    {
        // Calculate the vertical offset based on the player's Y position.
        float verticalOffset = (playerTransform.position.y / playerMaxY) * maxVerticalAdjust;
        float adjustedY = playerTransform.position.y + offset.y - verticalOffset;

        // Ensure the camera's Z position remains constant if you're using an offset.
        transform.position = new Vector3(playerTransform.position.x + offset.x, adjustedY, offset.z);
    }
}