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
    private Inventory inventory;
    private float scoreValue;
    private Timer timer;
    void Start()
    {
        scoreValue = 1000f;
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }

    public void CalculateScore()
    {
        scoreValue += mineralMultiplier * inventory.numberOfMinerals - timerMultiplier * timer.elapsedTime;
        scoreText.text = ((int)scoreValue).ToString();
    }
}
