using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int levelNumber;

    void Start()
    {
        PersistentDataManager.Instance.SetLastLevelPlayed(levelNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void GoToNextLevel()
    {
        // Obtain the current scene's build index and increment it to get the next level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the range of available scenes
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Load the next scene
            if (PersistentDataManager.Instance != null)
            {
                // Update the last level played in PersistentDataManager
                PersistentDataManager.Instance.SetLastLevelPlayed(nextSceneIndex);
            }
        }
        else
        {
            Debug.Log("No more levels. Returning to menu.");
            SceneManager.LoadScene("Menu"); // Adjust as necessary for your menu scene's name or index
        }
    }
}
   