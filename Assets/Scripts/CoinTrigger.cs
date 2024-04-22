using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // This function gets called when the coin should be collected.
    public void OnCollect()
    {
        animator.SetTrigger("Collect");
        // Consider adding a delay equal to the animation's length before destroying the coin, if needed.
        Destroy(gameObject, 0.5f); // Adjust the time based on the "CoinCollected" animation's length.
    }
}