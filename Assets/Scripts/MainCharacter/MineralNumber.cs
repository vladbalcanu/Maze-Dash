using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineralNumber : MonoBehaviour
{
    public Inventory inventory;
    public GameObject mainCharacter;
    public TextMeshProUGUI mineralNumber;

    private void Awake()
    {
        mainCharacter = GameObject.Find("Main Character");
        inventory = mainCharacter.GetComponent<Inventory>();
        mineralNumber = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        mineralNumber.text = inventory.numberOfMinerals.ToString();
    }
}
