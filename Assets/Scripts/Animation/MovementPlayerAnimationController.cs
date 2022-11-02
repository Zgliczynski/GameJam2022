using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayerAnimationController : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.MovementEvent += SetAnimationController;
    }

    private void OnDisable()
    {
        EventHandler.MovementEvent -= SetAnimationController;
    }

    private void SetAnimationController(float xInput, float yInput,
        float xMousePosition, float yMousePosition,
        bool isRunning, bool isWalking, bool isIdle, bool isDashing,
        bool idleLeftDown, bool idleLeftUp, bool idleRightDown, bool idleRightUp)
    {
        animator.SetFloat(Settings.xMousePosition, xMousePosition);
        animator.SetFloat(Settings.yMousePosition, yMousePosition);
        animator.SetFloat(Settings.xInput, xInput);
        animator.SetFloat(Settings.yInput, yInput);
        animator.SetBool(Settings.isRunning, isRunning);
        animator.SetBool(Settings.isWalking, isWalking);
        animator.SetBool(Settings.isDashing, isDashing);

        if (idleLeftDown)
            animator.SetTrigger(Settings.idleLeftDown);
        if (idleLeftUp)
            animator.SetTrigger(Settings.idleLeftUp);
        if (idleRightDown)
            animator.SetTrigger(Settings.idleRightDown);
        if (idleRightUp)
            animator.SetTrigger(Settings.idleRightUp);
    }


}
