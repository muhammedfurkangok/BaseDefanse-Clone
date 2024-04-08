using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera MainCamera;
    [SerializeField] private GameObject minigunTurret;
    [SerializeField] private PlayerPhysicController playerPhysicController;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CinemachineVirtualCamera TurretCamera;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private FloatingJoystick floatingJoystick;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerAnimator.SetBool("Running", false);
            playerPhysicController.enabled = false;
            inputManager.enabled = false;
            MainCamera.Priority = 0;
            TurretCamera.Priority = 1;
            TurretController();
            
            playerAnimator.SetBool("Turret", true);
           
        }
    }

    private void TurretController()
    {
        float horizontal = floatingJoystick.Horizontal;
        minigunTurret.transform.Rotate(Vector3.forward, horizontal * 100f * Time.deltaTime);
      playerManager.playerRb.velocity = Vector3.zero;
       
        

    }
}
