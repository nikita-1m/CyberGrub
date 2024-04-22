using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        PersistentDataManager.Instance.LoadLastLevel();
    }

    public void TryChangeSkin()
    {
        if (PersistentDataManager.Instance.GetCoinCount() >= 50)
        {
            PersistentDataManager.Instance.ToggleSkin();
        }
    }
}
