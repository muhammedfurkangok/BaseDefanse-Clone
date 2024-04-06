using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Animator playerAnimator;
   [ShowInInspector] public PlayerStates playerStates;
   [SerializeField] private GameObject gun;
    void Update()
    {
        
        switch(playerStates)
        {
            case PlayerStates.Idle:
                IdlePlayerAnimations();
                break;
            case PlayerStates.Fight:
                FightPlayerAnimations();
                break;
        }
    }

  
    private void IdlePlayerAnimations()
    {
        if(InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
        {
           playerAnimator.SetBool("Running",true); 
           playerAnimator.SetBool("Shoot",false);
           gun.SetActive(false);
        }
        else
        {
            playerAnimator.SetBool("Running",false);     
        }
    }
    private void FightPlayerAnimations()
    {
        if(InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
        {
            playerAnimator.SetBool("Running",true); 
            playerAnimator.SetBool("Shoot",true);
            gun.SetActive(true);
        }
        else
        {
            playerAnimator.SetBool("Running",false);     
        }
    }
}

    