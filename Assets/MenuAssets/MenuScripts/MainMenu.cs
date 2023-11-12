using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int sceneBuildIndex;
    public float startDelay = 0.3f;
    public void PlayGame()
    {
        StartCoroutine(NextScene());
    }
    public void QuitGame()
    {
        StartCoroutine(EndGame());
    }
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(startDelay);
        SceneManager.LoadScene(sceneBuildIndex);
    }
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(startDelay);
        Application.Quit();
    }
}

