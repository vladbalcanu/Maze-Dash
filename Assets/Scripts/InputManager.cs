using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float movementAmount;

    public float cameraInputX;
    public float cameraInputY;

    public float verticalInput;
    public float horizontalInput;

    public bool shift_input;
    public bool alt_input;
    public bool jump_input;
    public bool crouch_input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            // Cand apasam pe taste sau pe joystick stocam variabila in movementInput ce este un vector 2 de miscare (stanga-dreapta, sus-jos.
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.ShiftButton.performed += i => shift_input = true;
            playerControls.PlayerActions.ShiftButton.canceled += i => shift_input = false;

            playerControls.PlayerActions.AltButton.performed += i => alt_input = true;
            playerControls.PlayerActions.AltButton.canceled += i => alt_input = false;

            playerControls.PlayerActions.JumpButton.performed += i => jump_input = true;

            playerControls.PlayerActions.CrouchButton.performed += i => crouch_input = true;
            playerControls.PlayerActions.CrouchButton.canceled += i => crouch_input = false;

        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        if (PauseMenu.isPaused)
            return;
        HandleMovementInput();
        HandleCameraInput();
        HandleSprintingInput();
        HandleWalkingInput();
        HandleJumpInput();
        HandleCrouchInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        movementAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput)+ Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, movementAmount, playerLocomotion.isSprinting, playerLocomotion.isWalking, playerLocomotion.isCrouching);
    }

    private void HandleCameraInput()
    {
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
    }

    private void HandleSprintingInput()
    {
        if (shift_input) 
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleWalkingInput()
    {
        if (alt_input)
        {
            playerLocomotion.isWalking = true; 
        }
        else
        {
            playerLocomotion.isWalking = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJump();
        }
    }

    private void HandleCrouchInput()
    {
        if (crouch_input)
        {
            playerLocomotion.isCrouching = true;
        }
        else 
        {
            playerLocomotion.isCrouching = false;
        }

    }
}
