using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowFirecracker : MonoBehaviour
{

    [Header("References")]
    public Transform throwPoint;
    public Inventory inventory;
    public GameObject fireCracker;
    public GameObject mainCharacter;
    public MainCharacterAnimationManager mainCharacterAnimationManager;

    [Header("Settings")]
    public float cooldown;

    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
        mainCharacter = GameObject.Find("Main Character");
        inventory = mainCharacter.GetComponent<Inventory>();
        mainCharacterAnimationManager = mainCharacter.GetComponent<MainCharacterAnimationManager>();

    }

    private void Update()
    {
        if (readyToThrow && Keyboard.current.gKey.wasPressedThisFrame && inventory.UseFirecracker())
        {
            mainCharacterAnimationManager.PlayTargetAnimation("Throw", true);
            Invoke(nameof(Throw), 0.9f);
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        GameObject projectile = Instantiate(fireCracker, throwPoint.position, mainCharacter.transform.rotation);

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = mainCharacter.transform.forward * throwForce + transform.up * throwUpwardForce;
        
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        Invoke(nameof(ResetThrow), cooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
