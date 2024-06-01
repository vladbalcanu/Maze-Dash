using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public Inventory inventory { get; set; }

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        _prompt = "Use Box";
    }

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Opening Door");
        return true;
    }
}
