using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;
    [SerializeField] private float speed = 1f;

    public bool wasPushed;

    public Inventory inventory { get; set; }

    private Vector3 forward;
    private GameObject mainCharacter;
    private MainCharacterAnimationManager mainCharacterAnimationManager;
    private Vector3 mainCharacterPosition;
    private Coroutine animationCoroutine;

    public bool freeLeft, freeRight, freeForward;

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        forward = transform.forward;
        mainCharacter = GameObject.Find("Main Character");
        mainCharacterAnimationManager = mainCharacter.GetComponent<MainCharacterAnimationManager>();
        mainCharacterPosition = mainCharacter.transform.position;
        _prompt = "Push Box";
        wasPushed = false;
    }

    public bool Interact(Interactor interactor)
    {
        if (!wasPushed)
        {
            Vector3 direction;
            Vector3 targetPosition;
            wasPushed = true;
            if (freeLeft)
            {
                direction = Vector3.left ;
            }else if (freeRight)
            {
                direction = Vector3.right; 
            }else
            {
                direction = Vector3.forward;
            }

            targetPosition = gameObject.transform.position + direction * 1.2f;
            mainCharacterAnimationManager.PlayTargetAnimation("Push", true);
            animationCoroutine = StartCoroutine(MoveBox(targetPosition));

            return true;
        }
        else
        {
            _prompt = "Blocked";
            return false;
        }
    }

    private IEnumerator MoveBox(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float time = 0;
        while (time < 1)
        {

            transform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
