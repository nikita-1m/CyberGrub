using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounceForce = 10f; // The upward force to apply for a consistent bounce

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object's name is "Player"
        if (collision.gameObject.name == "Player")
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply a consistent upward force
                rb.velocity = new Vector2(rb.velocity.x, bounceForce);
            }
        }
    }
}
