using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public new GameObject camera;
    public float parallaxEffect; // The higher the value, the slower the background moves (min = 0, max = 1)
    private float startPosX, startPosY;

    void Start()
    {
        // Get starting camera position
        startPosX = transform.position.x; 
        startPosY = transform.position.y;
    }

    void Update()
    {
        // Calculate how much the background should move relative to the camera movement
        float distanceX = (camera.transform.position.x * parallaxEffect); 
        float distanceY = (camera.transform.position.y * parallaxEffect * 0.28f); // y parallax is weaker than x parallax

        transform.position = new Vector2(startPosX + distanceX, startPosY + distanceY);
    }
}
