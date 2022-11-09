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
    private bool FacingRight = true;
    private bool playerCollision = true;

    private float movmentSpeed;

    private Animator animator;

    //Movement Parameters
#pragma warning disable 0414
    private bool isIdle;
    private bool isRunning;
#pragma warning disable 0414
    private bool canDashing = true;

    //Dashing
    private float currentDashTime;

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
            isIdle = false;

            movmentSpeed = Settings.runningSpeed;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isIdle = false;

        }
        else if (xInput == 0 && yInput == 0)
        {
            isRunning = false;
            isIdle = true;
        }

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void AnimationController()
    {
        if (mousePosition.x < playerMove.x && FacingRight)
        {
            Flip();
        }
        else
        if (mousePosition.x > playerMove.x && !FacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        Vector3 tmpScale = transform.localScale;
        tmpScale.x = -tmpScale.x;
        transform.localScale = tmpScale;
        FacingRight = !FacingRight;
    }

}
        