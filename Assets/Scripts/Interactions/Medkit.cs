using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public bool hasBeenOpened;

    public Animator animator;

    public Inventory inventory { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hasBeenOpened = false;
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        _prompt = "Pick Medkit";
    }


    public bool Interact(Interactor interactor)
    {
        if (!hasBeenOpened)
        {
            Debug.Log("Opening Medkit");
            animator.CrossFade("opened_closed", 0.2f);
            inventory.pickMedkit();
            hasBeenOpened = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
