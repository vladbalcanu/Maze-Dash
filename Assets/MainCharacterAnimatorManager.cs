using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MainCharacterAnimationManager : MonoBehaviour
{
    public Animator animator;
    public AudioSource pickupAudioSource;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        pickupAudioSource = GetComponent<AudioSource>();
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        if (targetAnimation == "Pick")
        {
            pickupAudioSource.Play();
        }
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting, bool isWalking, bool isCrouching)
    {
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (isCrouching)
        {
            snappedHorizontal = 0.25f;
        }
        else if (horizontalMovement > 0 && horizontalMovement <= 0.55f)
        {
            snappedHorizontal = 0.5f;
        }else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement >= -0.55f )
        {
            snappedHorizontal = -0.5f;
        }else if (horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMovement > 0 && verticalMovement <= 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMovement < 0 && verticalMovement >= -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if (isSprinting && !isCrouching)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 2;
        }
        else if (isWalking && !isCrouching)
        {
            snappedHorizontal = horizontalMovement;
            snappedVertical = 0.5f;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }

}
