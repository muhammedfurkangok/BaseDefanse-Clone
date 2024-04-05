using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Transform playerChildrotation;
    [SerializeField] private float moveSpeed = 5f;

    #endregion

    #region Private Variables

    private float horizontal;
    private float vertical;

    #endregion

    #endregion

    private void Update()
    {
        GetInputs();
        SetRotation();
    }

    private void SetRotation()
    {
        if (horizontal != 0 || vertical != 0)
        {
            playerChildrotation.rotation = Quaternion.LookRotation(GetMovementInput());
        }
    }

    private void GetInputs()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
    }
    
    private void FixedUpdate()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        playerRb.velocity = GetMovementInput();
        //animation

    }

    private Vector3 GetMovementInput()
    {
        return new Vector3(horizontal, playerRb.velocity.y, vertical) * (moveSpeed * Time.fixedDeltaTime) ;
    }
}   
