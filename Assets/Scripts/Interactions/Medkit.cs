using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public bool hasBeenOpened;

    public Animator animator;
    public MainCharacterAnimationManager mainCharacterAnimationManager;
    public Inventory inventory { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hasBeenOpened = false;
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        mainCharacterAnimationManager = GameObject.Find("Main Character").GetComponent<MainCharacterAnimationManager>();
        _prompt = "Pick Medkit";
    }


    public bool Interact(Interactor interactor)
    {
        if (!hasBeenOpened)
        {
            mainCharacterAnimationManager.PlayTargetAnimation("Pick", true);
            animator.CrossFade("opened_closed", 0.2f);
            inventory.pickMedkit();
            hasBeenOpened = true;
            Invoke(nameof(DestroyBox), 2.0f);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DestroyBox()
    {
        gameObject.SetActive(false);
    }
}
