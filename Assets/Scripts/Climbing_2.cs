using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Climbing_2 : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder = false;
    private bool isClimbing = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D playerCollider;

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        // Check if the player is on the ladder and wants to climb
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        // Additional check for jumping off the ladder
        if (isClimbing && Input.GetButtonDown("Jump"))
        {
            isClimbing = false;
            isLadder = false; // Prevent immediately re-attaching to the ladder
            // Apply a small jump force when leaving the ladder
            rb.AddForce(new Vector2(0, speed * 0.75f), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // Remove gravity effect while climbing
            rb.velocity = new Vector2(0, vertical * speed); // Control vertical movement
        }
        else
        {
            rb.gravityScale = 4f; // Restore gravity effect when not climbing
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            // Delay resetting isLadder to prevent reattaching
            StartCoroutine(DelayedLadderExit());
        }
    }

    IEnumerator DelayedLadderExit()
    {
        yield return new WaitForSeconds(0.1f);
        isLadder = false;
        if (!isClimbing) // Only reset climbing state if not currently climbing
        {
            isClimbing = false;
        }
    }
}