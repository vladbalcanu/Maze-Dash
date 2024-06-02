using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string interactionPrompt => _prompt;

    public Inventory inventory { get; set; }

    public bool isOpen = false;
    public bool hasBeenUnlocked = false;
    [SerializeField] private bool isRotatingDoor;
    [SerializeField] private bool isKeylessDoor;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float forwardDirection = 0;
    [SerializeField] private float rotationAmount = 90f;

    public MainCharacterAnimationManager mainCharacterAnimationManager;
    public GameObject mainCharacter;
    public Vector3 mainCharacterPosition;

    private Vector3 StartRotation;
    private Vector3 forward;

    private Coroutine animationCoroutine;

    [Header("Sliding Configs")]
    private Vector3 SlideDirection = Vector3.up;
    [SerializeField]
    private float SlideAmount = 0.1f;

    private Vector3 StartPosition;
    public AudioSource? audioSource;

    private void Awake()
    {
        inventory = GameObject.Find("Main Character").GetComponent<Inventory>();
        if (!hasBeenUnlocked && !isKeylessDoor)
            _prompt = "Locked";
        else
            _prompt = "Open Door";
        StartRotation = transform.rotation.eulerAngles;
        forward = transform.forward;
        mainCharacter = GameObject.Find("Main Character");
        mainCharacterAnimationManager = mainCharacter.GetComponent<MainCharacterAnimationManager>();
        mainCharacterPosition = mainCharacter.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    public bool Interact(Interactor interactor)
    {
        if (isKeylessDoor)
        {
            UseDoor(mainCharacterPosition);
            return true;
        }
        else
        {
            if (hasBeenUnlocked)
            {
                UseDoor(mainCharacterPosition);
                return true;
            }
            else
            {
                if (inventory.useKey())
                {
                    UseDoor(mainCharacterPosition);
                    hasBeenUnlocked = true;
                    _prompt = "Open Door";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
    }

    public void UseDoor(Vector3 userPosition)
    {
        audioSource.Play();
        if (!isOpen)
        {
            if(animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (isRotatingDoor)
            {
                float dot = Vector3.Dot(forward, (userPosition - transform.position).normalized);
                mainCharacterAnimationManager.PlayTargetAnimation("Push", true);
                animationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                animationCoroutine = StartCoroutine(DoSlidingOpen());
            }


        }
        else
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (isRotatingDoor)
            {
                mainCharacterAnimationManager.PlayTargetAnimation("Pull Heavy Object", true);
                animationCoroutine = StartCoroutine(DoRotationClose());
            }
            else
            {
                animationCoroutine = StartCoroutine(DoSlidingClose());
            }


        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = transform.position + Vector3.up * SlideAmount;
        Vector3 startPosition = transform.position;
        float time = 0;
        isOpen = true;
        while(time < 1) 
        {

            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = transform.position + Vector3.down * SlideAmount;
        Vector3 startPosition = transform.position;
        float time = 0;
        isOpen = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (forwardAmount >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;

        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        isOpen = false;

        float time = 0;
        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }

    }

}
