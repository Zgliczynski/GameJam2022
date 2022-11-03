using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMonobehaviour<Player>
{
    //Movment
    private float xInput;
    private float yInput;
    private Vector2 mousePosition;
    private Vector2 playerMove;

    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private float movmentSpeed;

    private Animator animator;
    private float lastx, lasty;


    //Movement Parameters
    private bool isIdle;
    private bool isWalking;
    private bool isRunning;
    private bool isDashing;
    private bool idleLeftDown;
    private bool idleLeftUp;
    private bool idleRightDown;
    private bool idleRightUp;

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;
    }
    private void Update()
    {
        PlayerMovementInput();
        PlayerMovmentWalkInput();
        AnimationController();


        //mouse position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void FixedUpdate()
    {
        PlayerMovment();
    }

    private void PlayerMovment()
    {
        playerMove = new Vector2(xInput * movmentSpeed * Time.deltaTime, yInput * movmentSpeed * Time.deltaTime);
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        rb.MovePosition(rb.position + playerMove);
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if(xInput != 0 && yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;

            movmentSpeed = Settings.runningSpeed;
        }

        if(xInput !=0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;

        }
        else if(xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void PlayerMovmentWalkInput()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;

            movmentSpeed = Settings.walkingSpeed;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;

            movmentSpeed = Settings.runningSpeed;
        }
    }

    void AnimationController()
    {
        if (isRunning == true)
        {
            animator.SetFloat("xInput", playerMove.x);
            animator.SetFloat("yInput", playerMove.y);
            animator.SetFloat("mousePositionX", mousePosition.x);
            animator.SetFloat("mousePositionY", mousePosition.y);
        }

    }
}
