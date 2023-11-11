using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool GameIsPaused;
    // Start is called before the first frame update
    public GameObject PauseMenuUI;
    public GameObject GameUI;
    private float WaitTime = 0.5f;
    public void EndGame()
    {
        Debug.Log("You pressed quit");
        StartCoroutine(QuitGame());
    }
    private IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(WaitTime);
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused == true)
            {
                ResumeGame();
            }
            if(GameIsPaused == false)
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        StartCoroutine(Resume());
    }
    private IEnumerator Resume()
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(WaitTime);
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        GameUI.SetActive(true);
    }
    void PauseGame()
    {
        GameIsPaused = true;
        GameUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(NextScene());
    }
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene(0);
    }
}
