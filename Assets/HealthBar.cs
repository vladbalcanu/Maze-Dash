using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100;
    public float health;
    public int numberOfHits;
    public MainCharacterAnimationManager mainCharacterAnimationManager;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        mainCharacterAnimationManager = GameObject.Find("Main Character").GetComponent<MainCharacterAnimationManager>();
        numberOfHits = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
    }

    public void RecoverHealth(float medkitHealth)
    {
        health += medkitHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        numberOfHits++;
        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            mainCharacterAnimationManager.PlayTargetAnimation("Dying Backwards", true);
            Invoke(nameof(LoseGame), 4.0f);
        }
    }

    private void LoseGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
