 using System.Collections;
using System.Collections.Generic;
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
      
    
    #endregion

    #region Public Variables
    
     public float moveSpeed = 300f;

    #endregion

    #endregion
  
    
    
    
    
}
