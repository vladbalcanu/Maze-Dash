using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfMedkits;
    public int numberOfKeys;
    public int numberOfFirecrackers;
    public int numberOfMinerals;
    public HealthBar healthBar;

    private void Awake()
    {
        numberOfMedkits = 0;
        numberOfKeys = 0;
        numberOfFirecrackers = 0;
        numberOfMinerals = 0;
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    private void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            UseMedkit();
        }
    }

    public void PickMedkit()
    {
        numberOfMedkits++;
    }

    public void PickKey()
    {
        numberOfKeys++;
    }

    public void PickFirecracker()
    {
        numberOfFirecrackers++;
    }

    public void PickMineral()
    {
        numberOfMinerals++;
    }

    public bool UseKey()
    {
        if (numberOfKeys <= 0)
            return false;
        else
        {
            numberOfKeys--;
            return true;
        }
    }

    public bool UseMedkit()
    {
        if (numberOfMedkits <= 0 || healthBar.health == 100)
            return false;
        else
        {
            numberOfMedkits--;
            if (numberOfMedkits < 0)
                numberOfMedkits = 0;

            healthBar.RecoverHealth(25);
            return true;
        }
    }

    public bool UseFirecracker()
    {
        if (numberOfFirecrackers <= 0)
            return false;
        else
        {
            numberOfFirecrackers--;
            return true;
        }
    }
}
