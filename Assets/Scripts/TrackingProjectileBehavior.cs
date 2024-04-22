using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrackingProjectileBehavior : MonoBehaviour
{
    public float speed = 8f; // Projectile speed
    public float range = 13f; // Projectile range

    private Transform player; // Reference to player's location
    private float traveledDistance = 0f; // Distance projectile has traveled
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Calculate the direction towards the player at a slight delay
        InvokeRepeating("TrackPlayerPosition", 0f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the calculated direction
        transform.position += direction * Time.deltaTime * speed;

        traveledDistance += speed * Time.deltaTime;

        if (traveledDistance > range)
            Destroy(gameObject);
    }

    private void TrackPlayerPosition()
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
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
            playerScript.Die();
            Destroy(gameObject);
        }
    }
}

