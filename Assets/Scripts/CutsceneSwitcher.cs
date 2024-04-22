using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneSwitcher : MonoBehaviour
{
    public string targetScene; // Type name of cutscene scene in component

    private Image fadeImage;

    private void Start()
    {
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
    }

    // Load new scene when player collides with trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(LoadCutscene());
        }
    }

    // Makes screen fade to black and then loads associated cutscene for the level
    IEnumerator LoadCutscene()
    {
        // Fade scene to black
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            fadeImage.color = new Color(0, 0, 0, i); // Gradually increase the alpha of the black image
            yield return new WaitForSeconds(0.01667f);
        }
        fadeImage.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(targetScene); // Switch to the chosen cutscene
    }
}
