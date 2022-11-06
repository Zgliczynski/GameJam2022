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
    private SpriteRenderer sr;

    private float movmentSpeed;

    private Animator animator;

    //Movement Parameters
#pragma warning disable 0414
    private bool isIdle;
    private bool isWalking;
    private bool isRunning;
    private bool isDashing;
#pragma warning disable 0414

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;

    }
    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        PlayerMovementInput();
        PlayerMovmentWalkInput();
    }

    private void FixedUpdate()
    {
        PlayerMovment();
        AnimationController();
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

        if (xInput != 0 && yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;

            movmentSpeed = Settings.runningSpeed;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;

        }
        else if (xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void PlayerMovmentWalkInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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

    private void AnimationController()
    {
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

}