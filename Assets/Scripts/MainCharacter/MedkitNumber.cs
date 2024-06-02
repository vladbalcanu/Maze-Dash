using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MedkitNumber : MonoBehaviour
{
    public Inventory inventory;
    public GameObject mainCharacter;
    public TextMeshProUGUI medkitNumber;

    private void Awake()
    {
        mainCharacter = GameObject.Find("Main Character");
        inventory = mainCharacter.GetComponent<Inventory>();
        medkitNumber = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        medkitNumber.text = inventory.numberOfMedkits.ToString();
    }

}
