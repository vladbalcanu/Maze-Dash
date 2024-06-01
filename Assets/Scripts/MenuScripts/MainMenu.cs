using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Resolution()
    {
        SceneManager.LoadScene("ResolutionMenu");
    }

    public void Controls()
    {
        SceneManager.LoadScene("ControlsMenu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application has been closed");
    }

}