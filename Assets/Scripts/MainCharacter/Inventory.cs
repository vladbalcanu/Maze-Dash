using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberOfMedkits;
    public int numberOfKeys;
    public int numberOfFirecrackers;

    private void Awake()
    {
        numberOfMedkits = 0;
        numberOfKeys = 0;
        numberOfFirecrackers = 0;
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
        if (numberOfMedkits <= 0)
            return false;
        else
        {
            numberOfMedkits--;
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
