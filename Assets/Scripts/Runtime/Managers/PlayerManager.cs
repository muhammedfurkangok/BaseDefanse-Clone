 using System;
 using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Managers;
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
    
    #endregion

    #region Public Variables
    
     public float moveSpeed = 300f;

    #endregion

    #endregion

    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerHealthManager.Instance.OnPlayerDied += OnPlayerDied;
    }

    private async void OnPlayerDied()
    {
        playerAnimator.SetTrigger("Die");
        await UniTask.WaitForSeconds(4);
        transform.position = new Vector3(0, 0, -26);
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
