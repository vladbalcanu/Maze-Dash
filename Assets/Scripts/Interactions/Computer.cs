using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public GameObject attachedDoor;
    public Door door;

    public Inventory inventory { get; set; }

    private MainCharacterAnimationManager mainCharacterAnimationManager;

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        _prompt = "Push Button";
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        mainCharacterAnimationManager = GameObject.Find("Main Character").GetComponent<MainCharacterAnimationManager>();
        door = attachedDoor.GetComponent<Door>();
    }

    public bool Interact(Interactor interactor)
    {
        mainCharacterAnimationManager.PlayTargetAnimation("Button Pushing", true);
        door.UseDoor(transform.position);
        return true;
    }
}
