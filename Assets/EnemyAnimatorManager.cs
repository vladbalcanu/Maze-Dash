using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
         animator.SetBool("isInteracting", isInteracting);
         animator.CrossFade(targetAnimation, 0.2f);
        
    }

    public void UpdateAnimatorValues(bool isPatroling, bool isChasing, bool isAttacking)
    {
        float snappedHorizontal = 0.0f;
        float snappedVertical = 0.0f;

        if (isPatroling)
        {
            snappedVertical = 0.0f;
        }else if (isChasing)
        {
            snappedVertical = 0.25f;
        }else if (isAttacking)
        {
            snappedVertical = 0.50f;
        }
       
        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
    }
}
