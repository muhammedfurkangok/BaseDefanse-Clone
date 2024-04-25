 using System;
 using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Managers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Self Varibles

    #region Serialized Variables

    [SerializeField] internal PlayerAnimationController playerAnimationController;
    [SerializeField] internal PlayerMeshController playerMeshController;
    [SerializeField] internal PlayerPhysicController playerPhysicController;
    [SerializeField] internal InputManager inputManager;
    
    [SerializeField] public Rigidbody playerRb;
    [SerializeField] public Transform playerChildrotation;
    [SerializeField] public Transform playerStackrotation;
      
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject child;
    
    #endregion

    #region Public Variables
    
     public float moveSpeed = 300f;

    #endregion

    #endregion

    private void Start()  
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerHealthManager.Instance.OnPlayerDied += OnPlayerDied;
    }

    private async void OnPlayerDied()
    {
        enabled = false;
        playerAnimator.SetTrigger("Die");
        await UniTask.WaitForSeconds(2);
        enabled = true;
        playerAnimator.SetTrigger("Idle");
        playerAnimator.SetBool("Shoot",false);
        PlayerHealthManager.Instance.health = 100;
        PlayerHealthManager.Instance.TakeDamage(0);
        transform.position = new Vector3(0.2f, 0, -26);
    }
    
    private void UnsubscribeEvents()
    {
        PlayerHealthManager.Instance.OnPlayerDied -= OnPlayerDied;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}
