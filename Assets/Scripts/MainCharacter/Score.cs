using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private float mineralMultiplier;
    [SerializeField] private float timerMultiplier;
    [SerializeField] private float sneakyMultiplier;
    private Inventory inventory;
    private float startScoreValue;
    private Timer timer;
    void Start()
    {
        startScoreValue = 10000f;
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    public void CalculateScore()
    {
        startScoreValue += mineralMultiplier * inventory.numberOfMinerals - timerMultiplier * timer.elapsedTime - sneakyMultiplier * inventory.healthBar.numberOfHits;
        scoreText.text = ((int)startScoreValue).ToString();
    }
}
