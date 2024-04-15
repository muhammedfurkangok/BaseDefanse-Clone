using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPhysicController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private PlayerAimController playerAimController;
    

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
        if (playerAnimationController.playerStates == PlayerStates.Fight)
        {
            if (playerAimController.currentEnemy != null)
            {
                Vector3 direction = playerAimController.currentEnemy.transform.position - playerManager.playerChildrotation.position;
                playerManager.playerChildrotation.rotation = Quaternion.LookRotation(direction);
                playerManager.playerStackrotation.rotation = Quaternion.LookRotation(direction);
            }
            else if (InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
            {
                Vector3 movementInput = InputManager.instance.GetMovementInput();
                playerManager.playerChildrotation.rotation = Quaternion.LookRotation(movementInput);
                playerManager.playerStackrotation.rotation = Quaternion.LookRotation(movementInput);
            }
        }
        else if (InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
        {
            Vector3 movementInput = InputManager.instance.GetMovementInput();
            playerManager.playerChildrotation.rotation = Quaternion.LookRotation(movementInput);
            playerManager.playerStackrotation.rotation = Quaternion.LookRotation(movementInput);
        }
    }

    
}
