using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public GameObject finishMenu;
    public GameObject mainCharacter;
    public Score score;
    public LayerMask starshipMask;
    public float winRadius;
    public bool gameFinished;

    private void Start()
    {
        finishMenu.SetActive(false);
        mainCharacter = GameObject.Find("Main Character");
        winRadius = 5.0f;
        gameFinished = false;
        score = GameObject.Find("UI").GetComponent<Score>();
    }

    void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(mainCharacter.transform.position, winRadius, starshipMask);
        if (rangeChecks.Length > 0 && gameFinished == false)
        {
            FinishGame();
        }
    }

    public void FinishGame()
    {
        Time.timeScale = 0f;
        score.CalculateScore();
        gameFinished = true;
        finishMenu.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }

    public void ResumeGame()
    {
        finishMenu.SetActive(false);
        Time.timeScale = 1f;
        Invoke(nameof(ResetGameFinished), 300f);
    }

    private void ResetGameFinished()
    {
        gameFinished = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
