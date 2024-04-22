using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour
{
    public float moveSpeed = 2f; // Movement speed of the robot.
    public float moveDistance = 3f; // Distance from the starting point the robot moves before turning around.

    public Collider2D bodyCollider; // Assign in the inspector.
    public Collider2D headCollider; // Assign in the inspector, set as a trigger.
    public Animator animator; // Assign in the inspector.

    private Rigidbody2D rb;
    private float traveledDistance = 0f;
    private bool movingLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveBackAndForth();
    }

    // Controls enemy movement
    private void MoveBackAndForth()
    {
        // Move enemy either left or right
        if (movingLeft)
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        else if (!movingLeft)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        // Update total distance travelled
        traveledDistance += moveSpeed * Time.deltaTime;

        // Swap moving direction once target distance reached
        if (traveledDistance > moveDistance)
        {
            FlipSprite();
            movingLeft = !movingLeft;
            traveledDistance = 0f;
        }
    }

    // Flips the sprite by scaling in opposite direction
    private void FlipSprite()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
    // Sequence of events for when enemy is destroyed
    private void DestroyAnimation()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(gameObject, 0.3f); // Destroys the robot
        animator.SetTrigger("Destroy"); // Triggers the destroy animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kills Player
        if (collision.otherCollider == bodyCollider && collision.gameObject.name == "Player")
        {
            Debug.Log("Player has collided with the robot's body.");
            Movement playerScript = collision.gameObject.GetComponent<Movement>();
            playerScript.Die(); 
        }

        // Destroys robot
        if (collision.otherCollider == headCollider && collision.gameObject.name == "Player")
        {
            Debug.Log("Player has collided with the robot's head.");

            // Make player bounce on collision
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.velocity = new Vector2(rb.velocity.x, 15f);

            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true); // Disable collisions with player
            gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Change enemy colour to red

            // Destroy the enemy after delay
            Invoke("DestroyAnimation", 0.25f);
        }
    }
}
