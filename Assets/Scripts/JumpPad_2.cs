using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad_2 : MonoBehaviour
{
    public float jumpForce = 10f; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        
        
        if (rb != null)
        {
         
            Vector2 force = new Vector2(0, jumpForce);
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}