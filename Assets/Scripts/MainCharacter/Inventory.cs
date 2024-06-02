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
    public int itemPicker;
    public HealthBar healthBar;

    private void Awake()
    {
        numberOfMedkits = 0;
        numberOfKeys = 0;
        numberOfFirecrackers = 0;
        itemPicker = 0;
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            itemPicker = 0;
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            itemPicker = 1;
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            itemPicker = 2;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            switch (itemPicker)
            {
                case 0:
                    useMedkit();
                    break;
                case 1:
                    //useFirecracker();
                    break;
                case 2:
                    //Debug.Log("Key");
                    break;
            }
        }
    }

    public void pickMedkit()
    {
        numberOfMedkits++;
    }

    public void pickKey()
    {
        numberOfKeys++;
    }

    public void pickFirecracker()
    {
        numberOfFirecrackers++;
    }

    public bool useKey()
    {
        if (numberOfKeys <= 0)
            return false;
        else
        {
            numberOfKeys--;
            return true;
        }
    }

    public bool useMedkit()
    {
        if (numberOfMedkits <= 0 || healthBar.health == 100)
            return false;
        else
        {
            numberOfMedkits--;
            healthBar.health += 25;
            if (healthBar.health > 100)
                healthBar.health = 100;
            return true;
        }
    }

    public bool useFirecracker()
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
