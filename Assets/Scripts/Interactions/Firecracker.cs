using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecracker : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public MainCharacterAnimationManager mainCharacterAnimationManager;
    public Inventory inventory { get; set; }


    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        mainCharacterAnimationManager = GameObject.Find("Main Character").GetComponent<MainCharacterAnimationManager>();
        _prompt = "Pick Firecracker";
    }

    public bool Interact(Interactor interactor)
    {
        mainCharacterAnimationManager.PlayTargetAnimation("Pick", true);
        inventory.pickFirecracker();
        Invoke(nameof(DestroyFirecracker), 0.2f);
        return true;
    }

    private void DestroyFirecracker()
    {
        gameObject.SetActive(false);
    }
}
