using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 15f; // Projectile speed
    public float range = 15f; // Projectile range

    private float traveledDistance = 0f; // Distance projectile has traveled

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
        traveledDistance += speed * Time.deltaTime;

        if (traveledDistance > range)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroys on collision with Environment
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            Destroy(gameObject); 
        }

        // Ignore collisions if colliding with projectile or mob
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Projectile")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true);
        }

        // Kill player on collision
        if (collision.gameObject.tag == "Player")
        {
            Movement playerScript = collision.gameObject.GetComponent<Movement>();
            if (!playerScript.isInvincible)
            {
                playerScript.Die();
            }
            Destroy(gameObject);
        }
    }
}

