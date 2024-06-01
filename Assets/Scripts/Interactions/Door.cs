using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public Inventory inventory { get; set; }

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        _prompt = "Open Door";
    }

    public bool Interact(Interactor interactor)
    {
        if (inventory.useKey())
        {
            Debug.Log("Opening Door");
            return true;
        }
        else
        {
            Debug.Log("You don't have any keys");
            return false;
        }
        
    }
}
