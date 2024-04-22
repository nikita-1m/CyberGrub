using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public float speed = 15f;
    public float distance = 30f;

    private float traveledDistance = 0f;
    private Rigidbody2D rb;
    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the car to the left
        rb.velocity = new Vector2(-speed, rb.velocity.y);

        // Update total distance travelled
        traveledDistance += speed * Time.deltaTime;

        // Reset car position after desired distance reached
        if (traveledDistance > distance)
        {
            transform.position = startPosition;
            traveledDistance = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kills Player on collisions
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player has collided with the enemy's body.");
            Movement playerScript = collision.gameObject.GetComponent<Movement>();
            playerScript.Die();
        }
    }
};
