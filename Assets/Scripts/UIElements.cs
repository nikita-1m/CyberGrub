using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UISettings : MonoBehaviour
{
    public Text exampleText; 
    public GameObject timerObject; 
    public GameObject subtitlesObject; 
    public Canvas uiElementsCanvas; 


    public void AdjustTextSize(float size)
    {
        exampleText.fontSize = Mathf.RoundToInt(size);
    }

  
    public void ToggleTimer(bool isVisible)
    {
        timerObject.SetActive(isVisible);
    }

  
    public void ToggleSubtitles(bool isVisible)
    {
        subtitlesObject.SetActive(isVisible);
    }

   
    public void ToggleUIElements(bool isVisible)
    {
        uiElementsCanvas.gameObject.SetActive(isVisible);
    }
}

