using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movment
    private float xInput;
    private float yInput;

    private Camera mainCamera;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    public float movmentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        mainCamera = Camera.main;
    }
    private void Update()
    {
        PlayerMovementInput();
    }

    private void FixedUpdate()
    {
        PlayerMovment();
    }

    private void PlayerMovment()
    {
        Vector2 move = new Vector2(xInput * movmentSpeed * Time.deltaTime, yInput * movmentSpeed * Time.deltaTime);

        rb.MovePosition(rb.position + move);
    }

    private void PlayerMovementInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        
    }
}
