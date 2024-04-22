using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrackingDroneBehavior : MonoBehaviour
{
    public bool HorizontalMovement = false;
    public bool VerticalMovement = false;
    public float moveSpeed = 2f;
    public float moveDistance = 4f;
    public float attackDelay = 1.5f;

    public TrackingProjectileBehavior projectile;
    public Transform projectileSource;
    public Animator animator;

    private Rigidbody2D rb;
    private float traveledDistance = 0f;
    private bool movingUp = true;
    private bool movingLeft = true;
    private bool isKilled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Fires bullets at constant delay
        InvokeRepeating("RangedAttack", 0f, attackDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (HorizontalMovement)
        {
            MoveHorizontal();
        }
        if (VerticalMovement)
        {
            MoveVertical();
        }
    }

    // Controls vertical enemy movement
    private void MoveVertical()
    {
        // Move enemy either up or down
        if (movingUp)
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
        else if (!movingUp)
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);

        // Update total distance travelled
        traveledDistance += moveSpeed * Time.deltaTime;

        // Swap moving direction once target distance reached
        if (traveledDistance > moveDistance)
        {
            movingUp = !movingUp;
            traveledDistance = 0f;
        }
    }

    // Controls horizontal enemy movement
    private void MoveHorizontal()
    {
        if (movingLeft)
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        else if (!movingLeft)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        traveledDistance += moveSpeed * Time.deltaTime;

        if (traveledDistance > moveDistance)
        {
            movingLeft = !movingLeft;
            traveledDistance = 0f;
        }
    }

    // Creates laser bullet projectiles
    private void RangedAttack()
    {
        if (!isKilled)
        {
            // Instantiate the projectile GameObject
            GameObject newProjectile = Instantiate(projectile.gameObject, projectileSource.position, transform.rotation);

            // Set the parent of the new projectile to this drone
            newProjectile.transform.parent = transform;
        }
    }

    // Sequence of events for when enemy is destroyed
    private void DestroySequence()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(gameObject, 0.3f); // Destroys the robot
        animator.SetTrigger("Destroy"); // Triggers the destroy animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player has collided with the enemy's body.");
            Movement playerScript = collision.gameObject.GetComponent<Movement>();

            // Kill drone if dashed into
            if (playerScript.isDashing)
            {
                isKilled = true;
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true); // Disable collisions with player
                gameObject.GetComponent<SpriteRenderer>().color = Color.red; // Change enemy colour to red
                Invoke("DestroySequence", 0.25f);
            }
            // Kill Player on collision if not dashing
            else
            {
                playerScript.Die();
            }
        }
    }
}
