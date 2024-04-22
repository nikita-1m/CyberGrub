using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehavior : MonoBehaviour
{
    // Kill Player on collision if not dashing
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player has collided with the Obstacle's body.");
            Movement playerScript = collision.gameObject.GetComponent<Movement>();
            playerScript.Die();
        }
    }
}
