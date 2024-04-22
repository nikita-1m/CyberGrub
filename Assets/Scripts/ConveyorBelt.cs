using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 2.5f; 

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
            Vector2 velocity = rb.velocity;
            velocity.x = speed;
            rb.velocity = velocity;
        }
    }
}
