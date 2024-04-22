using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentDataManager : MonoBehaviour
{
    public static PersistentDataManager Instance;

    public Color playerColor = Color.white; // Default player color
    // Define two example colors for the skins
    private Color skin1 = new Color(1f, 1f, 1f, 1f);
    private Color skin2 = new Color(1f, 0f, 1f, 1f);
    private int coinCount = 0; // Tracks the number of coins collected
    public int lastLevelPlayed = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void AddCoins(int amount)
    {
        coinCount += amount;
    }

    // Method to retrieve the current coin count
    public int GetCoinCount()
    {
        return coinCount;
    }

    public void ToggleSkin()
    {
        // Toggle between skin1 and skin2
        playerColor = playerColor.Equals(skin1) ? skin2 : skin1;
    }

    public void LoadLastLevel()
    {
        SceneManager.LoadScene("Level" + lastLevelPlayed);
    }

    public void SetLastLevelPlayed(int level)
    {
        lastLevelPlayed = level;
    }
}