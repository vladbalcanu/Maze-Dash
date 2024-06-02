using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartInstructions : MonoBehaviour
{
    public GameObject startInstructions;
    public static bool isPaused;

    private void Start()
    {
        startInstructions.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        startInstructions.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }
}
