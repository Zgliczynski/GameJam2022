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

    //Parameters
    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private TrailRenderer tr;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private bool FacingRight = true;

    //Animator
    private Animator animator;

    //Dashing Parameters
    private bool canDashing = true;
    private bool isDashing;

    //Jumping Parameters
    private int extraJump = 1;
    private int jumpCount = 0;
    private bool isGrounded;


    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();

        mainCamera = Camera.main;

    }
    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        PlayerMovementInput();
        PlayerMovment();
        FlipController();
        PlayerMovementDashInput();
    }

    private void PlayerMovment()
    {
        rb.velocity = new Vector2(xInput * Settings.runningSpeed, rb.velocity.y);
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && jumpCount < extraJump)
        {
            Jump();
            jumpCount++;
        }
    }

    private void PlayerMovementDashInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    //Flip
    private void FlipController()
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

    //Dash
    private IEnumerator Dash()
    {
        canDashing = false;
        isDashing = true;
        float orginalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x * Settings.playerDashSpeed, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(Settings.dashTime);
        tr.emitting = false;
        rb.gravityScale = orginalGravity;
        isDashing = false;
        yield return new WaitForSeconds(Settings.dashingCooldown);
        canDashing = true;
    }

    //Jump
    private void CheckGrounded()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer))
        {
            jumpCount = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, Settings.jumpingForce);
    }
}
        