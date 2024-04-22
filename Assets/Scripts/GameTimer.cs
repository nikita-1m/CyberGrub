using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText; 
    public Text bestTimeText; 
    private float startTime;
    private bool isRunning = false;
    private float currentTime = 0f;

    private const string BestTimePrefKey = "BestTime"; // for storing best time 

    void Start()
    {
        timerText.text = "00:00";
        DisplayBestTime();
    }

    public void StartTimer()
    {
        isRunning = true;
        startTime = Time.time;
    }

    public void StopTimer()
    {
        isRunning = false;
        float finalTime = currentTime;
        SaveBestTime(finalTime);
        DisplayBestTime();
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime = Time.time - startTime;
            timerText.text = FormatTime(currentTime);
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        float seconds = time % 60;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    void SaveBestTime(float finalTime)
    {
        float bestTime = PlayerPrefs.GetFloat(BestTimePrefKey, float.MaxValue);
        if (finalTime < bestTime)
        {
            PlayerPrefs.SetFloat(BestTimePrefKey, finalTime);
            PlayerPrefs.Save();
        }
    }

    void DisplayBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat(BestTimePrefKey, float.MaxValue);
        if (bestTime == float.MaxValue)
        {
            bestTimeText.text = "Best Time: --:--";
        }
        else
        {
            bestTimeText.text = "Best Time: " + FormatTime(bestTime);
        }
    }
}
