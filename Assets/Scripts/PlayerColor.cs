using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour

{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyColor();
    }

    void ApplyColor()
    {
        if (PersistentDataManager.Instance != null)
        {
            spriteRenderer.color = PersistentDataManager.Instance.playerColor;
        }
    }
}