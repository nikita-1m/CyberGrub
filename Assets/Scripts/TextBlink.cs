using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    public float blinkTime = 0.5f; // Time in seconds for each blink
    private TMP_Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        StartCoroutine(Blink());
    }

    void OnDisable()
    {
        StopCoroutine(Blink());
        textComponent.enabled = true; // Ensure the text is enabled when the object is disabled
    }

    IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
    }
}