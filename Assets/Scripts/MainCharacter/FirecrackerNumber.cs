using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirecrackerNumber : MonoBehaviour
{
    public Inventory inventory;
    public TextMeshProUGUI firecrackerNumber;

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        firecrackerNumber = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        firecrackerNumber.text = inventory.numberOfFirecrackers.ToString();
    }
}
