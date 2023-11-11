using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int SceneBuildIndex;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneBuildIndex);
    }
    public void QuitGame()
    {
        if(UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {Application.Quit();}
    }
}
