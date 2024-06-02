using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyNumber : MonoBehaviour
{
    public Inventory inventory;
    public GameObject mainCharacter;
    public TextMeshProUGUI keyNumber;

    private void Awake()
    {
        mainCharacter = GameObject.Find("Main Character");
        inventory = mainCharacter.GetComponent<Inventory>();
        keyNumber = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        keyNumber.text = inventory.numberOfKeys.ToString();
    }
}
