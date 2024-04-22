using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Movement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 20f; // Speed of the dash
    public float dashTime = 0.3f; // Duration of the dash

    [HideInInspector] public bool isDashing = false; // Track if the player is currently dashing
    [HideInInspector] public bool isInvincible = false; // Track if the player is current invincible

    private Rigidbody2D rb;
    private TrailRenderer trail;
    private bool isGrounded = false;
    private int jumpCount = 0;
    private bool hasDashed = false; // Track if the player has dashed
    private float lastHorizontalInput = 0; // Track the last direction of horizontal input

    private Vector3 originalScale; // Original scale of the player
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Set up the trail renderer
        trail = GetComponent<TrailRenderer>();
        trail.time = dashTime;
        trail.enabled = false;

        originalScale = transform.localScale; // Store the original scale
        ApplyColor();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            lastHorizontalInput = moveInput; // Update last direction based on input
        }

        Move(moveInput);
        Jump();

        if (SceneManager.GetActiveScene().name != "Level1") // Unlock dash after level 1
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                StartCoroutine(Dash());
            }
        }


        // Flip the player sprite based on movement direction
        if (moveInput > 0)
        {
            transform.localScale = originalScale; // Set original scale (facing right)
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Flip horizontally (facing left)
        }
    }


    void Move(float moveInput)
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "Level1") 
            {
                if (isGrounded || jumpCount < 1)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isGrounded = false;
                    jumpCount++;
                }
            }
            else // Unlock double jump after level 1
            {
                if (isGrounded || jumpCount < 2)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isGrounded = false;
                    jumpCount++;
                }
            }

        }
    }
    public void Die()
    {
        if (!isInvincible)
        {
            animator.SetTrigger("Die"); // Trigger the death animation

            
            StartCoroutine(ReloadLevelAfterDelay(0.2f)); // Delay for 1 second, adjust as needed
        }
    }

    IEnumerator ReloadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Dash()
    {
        if (!hasDashed)
        {
            isInvincible = true;
            float startTime = Time.time; // Time when the dash starts
            while (Time.time < startTime + dashTime)
            {
                trail.enabled = true; // Make trail appear when dashing
                isDashing = true;
                rb.velocity = new Vector2(dashSpeed * lastHorizontalInput, rb.velocity.y);
                yield return null; // Wait for the next frame
            }
            hasDashed = true;
            yield return new WaitForSeconds(0.2f);
            trail.enabled = false;
            isDashing = false;
            isInvincible = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            // Iterate through all contact points
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Check if the normal of the contact point is roughly pointing upwards
                if (contact.normal.y > 0.5) // Adjust the value based on your needs, 0.5 ensures a mostly upward direction
                {
                    isGrounded = true;
                    jumpCount = 0; // Reset the jump count when the player is on top of the ground
                    hasDashed = false; // Reset the dash ability when the player is on top of the ground
                    break; // Exit the loop as we found a contact point indicating we're on top of the ground
                }
            }
        }

        // Reset jump count & dash whenever enemy is touched (incase you kill it)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isGrounded = true;
            jumpCount = 0;
            hasDashed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ladder"))
        {
            isGrounded = true;
            jumpCount = 0; // Reset the jump count when the player touches the ground or ladder
            hasDashed = false; // Reset the dash ability when the player touches the ground or ladder
        }
        else if (collider.gameObject.CompareTag("Coin"))
        {
            CoinTrigger coin = collider.gameObject.GetComponent<CoinTrigger>();
            if (coin != null)
            {
                coin.OnCollect();
                Destroy(collider.gameObject, 0.3f);
                PersistentDataManager.Instance.AddCoins(1);
                Debug.Log("Coins collected: " + PersistentDataManager.Instance.GetCoinCount());
            }
        }
    }

    void ApplyColor()
    {
        // Apply the saved player color if PersistentDataManager exists
        if (PersistentDataManager.Instance != null)
        {
            spriteRenderer.color = PersistentDataManager.Instance.playerColor;
        }
    }
}