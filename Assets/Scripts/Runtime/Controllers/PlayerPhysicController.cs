using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    

    void Update()
    {
        SetRotation();
       
    }

    private void FixedUpdate()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        playerManager.playerRb.velocity = InputManager.instance.GetMovementInput();
    }


    private void SetRotation()
    {
        if (playerAnimationController.playerStates == PlayerStates.Idle)
        {
            if (InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
            {
                playerManager.playerChildrotation.rotation =
                    Quaternion.LookRotation(InputManager.instance.GetMovementInput());
                playerManager.playerStackrotation.rotation =
                    Quaternion.LookRotation(InputManager.instance.GetMovementInput());
            }
        }
    }
    
    
}
