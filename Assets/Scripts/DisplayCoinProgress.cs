using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCoinProgress : MonoBehaviour
{
    private TextMeshProUGUI coinProgressText;
    [SerializeField] private int totalCoinsNeededToUnlockSkin = 10; // Set this based on your game's logic

    void Start()
    {
        coinProgressText = GetComponent<TextMeshProUGUI>();
        UpdateCoinProgressText();
    }
    private void Update()
    {
        UpdateCoinProgressText();
    }

    void UpdateCoinProgressText()
    {
        if (PersistentDataManager.Instance != null)
        {
            int currentCoins = PersistentDataManager.Instance.GetCoinCount();
            coinProgressText.text = $"{currentCoins} / {totalCoinsNeededToUnlockSkin}";
        }
        else
        {
            coinProgressText.text = "Error: Data not available";
        }
    }
}