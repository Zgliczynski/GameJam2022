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
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform wallCheck_0;
    [SerializeField] private LayerMask wallLayer;


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

    //Wall Jump & Wall Slide
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(7f, 11.5f);

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
        WallSlide();
        WallJump();
        PlayerMovementInput();
        if (!isWallJumping)
        {
            PlayerMovment();
        }
        FlipController();
    }

    private void FixedUpdate()
    {
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

    //Wall Slide
    //Don't know how I can do it else, maybe latter
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private bool IsWalled_0()
    {
        return Physics2D.OverlapCircle(wallCheck_0.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(IsWalled() && !isGrounded && xInput != 0 || IsWalled_0() && !isGrounded && xInput != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Settings.wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    //Wall Jump
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }


    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}
        