using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMovement : MonoBehaviour

{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyColor();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyColor();
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
