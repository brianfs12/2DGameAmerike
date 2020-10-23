﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(1, 100)]
    public float jumpVelocity;

    [Range(1, 20)]
    public float movementSpeed;

    public float GroundDistance = 0.2f;
    public LayerMask Ground;

    private Rigidbody rb;
    private bool isGrounded = true;
    private Transform groundChecker;
    private Vector3 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //The first child should always be an empty object. We'll use this to check if the player is touching the ground.
        groundChecker = transform.GetChild(0);
    }

    private void Update()
    {
        //Checks if the character is grounded.
        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        movementInput = Vector3.zero;
        //Cambiar controles segun la perspectiva de la camara
        if(Camera_Transition.ortho)
        {
            movementInput.z = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.z = Input.GetAxisRaw("Vertical");
        }
        
        if (movementInput != Vector3.zero)
        {
            transform.forward = movementInput;
        }
        if (Input.GetButtonDown("Button A") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        //ClampMagnitude clamps the magnitude of the vector so that diagonal movement won't be faster than horizontal or vertical movement.
        rb.MovePosition(rb.position + Vector3.ClampMagnitude(movementInput * movementSpeed, 6f) * Time.fixedDeltaTime);
    }

    void Jump()
    {
        rb.velocity = Vector3.up * jumpVelocity;
    }
}
