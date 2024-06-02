using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public MainCharacterAnimationManager mainCharacterAnimationManager;
    public Inventory inventory { get; set; }


    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        mainCharacterAnimationManager = GameObject.Find("Main Character").GetComponent<MainCharacterAnimationManager>();
        _prompt = "Pick Key";
    }

    public bool Interact(Interactor interactor)
    {
        mainCharacterAnimationManager.PlayTargetAnimation("Pick", true);
        inventory.pickKey();
        Invoke(nameof(DestroyKey), 0.2f);
        return true;
    }

    private void DestroyKey()
    {
        gameObject.SetActive(false);
    }
}
