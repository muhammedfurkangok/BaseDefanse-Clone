using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton

    public static InputManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    #region Serialized Variables

    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private PlayerManager playerManager;

    #endregion
    #region Private Variables

    public float horizontal;
    public float vertical;

    #endregion

    private void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
    }
    
    public Vector3 GetMovementInput()
    {
        return new Vector3(horizontal, playerManager.playerRb.velocity.y, vertical) * (playerManager.moveSpeed * Time.fixedDeltaTime) ;
    }
}
