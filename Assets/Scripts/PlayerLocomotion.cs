using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    Vector3 movementDirection;
    Transform cameraObject;
    Rigidbody playerRigidBody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 1.0f;
    public float inAirForwardVelocity = 0.5f;
    public LayerMask groundLayer;


    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;
    public bool isCrouching;
    public bool isHit;

    [Header("Movement Speeds")]
    public float crouchingSpeed = 1.5f;
    public float walkingSpeed = 3;
    public float runningSpeed = 5;
    public float sprintingSpeed = 8;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isJumping)
            return;

        if (isHit)
        {
            return;
        }

        movementDirection = cameraObject.forward * inputManager.verticalInput; //Movement Input
        movementDirection = movementDirection + cameraObject.right * inputManager.horizontalInput;
        movementDirection.Normalize();
        movementDirection.y = 0;

        if (isCrouching)
        {
            movementDirection = movementDirection * crouchingSpeed;
        }
        else if (isSprinting)
        {
            movementDirection = movementDirection * sprintingSpeed;
        }
        else if (isWalking)
        {
            movementDirection = movementDirection * walkingSpeed;
        }
        else
        {
            if (inputManager.movementAmount >= 0.5f)
            {
                movementDirection = movementDirection * runningSpeed;
            }
            else
            {
                movementDirection = movementDirection * walkingSpeed;
            }
        }

        Vector3 movementVelocity = movementDirection;
        playerRigidBody.velocity = movementVelocity;

    }

    private void HandleRotation()
    {

        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection== Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(fallingVelocity * inAirTimer * -Vector3.up);

            movementDirection = cameraObject.forward * inputManager.verticalInput; //Movement Input
            movementDirection = movementDirection + cameraObject.right * inputManager.horizontalInput;
            movementDirection.Normalize();
            movementDirection.y = 0;
            movementDirection = movementDirection * inAirForwardVelocity;
            Vector3 movementVelocity = movementDirection;
            playerRigidBody.velocity = movementVelocity;
        }

        if (Physics.SphereCast(rayCastOrigin, 0.1f, -Vector3.up, out hit, groundLayer)) 
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }
            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.movementAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJump()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = movementDirection ;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
    }

    public void HandleHit()
    {
        if (isGrounded)
        {
            print("Got Hit");
            animatorManager.animator.SetBool("isHit", true);
            animatorManager.PlayTargetAnimation("Hit", false);
        }
    }
}
