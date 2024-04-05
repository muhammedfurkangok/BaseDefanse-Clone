using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private Animator playerAnimator;
    void Update()
    {
        PlayerAnimations();
    }

    private void PlayerAnimations()
    {
        if(InputManager.instance.horizontal != 0 || InputManager.instance.vertical != 0)
        {
           playerAnimator.SetBool("isRunning", true);
        }
        else
        {
           playerAnimator.SetBool("isRunning", false);
        }
    }
}
