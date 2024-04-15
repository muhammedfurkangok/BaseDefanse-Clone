// TurretManager.cs

using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
 

    [SerializeField] private CinemachineVirtualCamera MainCamera;
    [SerializeField] private CinemachineVirtualCamera TurretCamera;
    [SerializeField] private GameObject minigunTurret;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject FakePlayer;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private PlayerPhysicController playerPhysicController;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject playerMesh;
    [SerializeField] private TurretAmmoManager turretAmmoManager;
    
    public bool isTurretExit = false;
   

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ontriggerenter");
            if (turretAmmoManager.HasAmmo())
            {
                await turretAmmoManager.ShootBullets();
            }
            isTurretExit = false;
        }
    }

    private  void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TurretController();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("ontriggerexit");
        if (other.CompareTag("Player"))
        {
            isTurretExit = true;
        }
    }

    private void SetTurretCamera()
    {
        MainCamera.Priority = 0;
        TurretCamera.Priority = 5;
    }

    private void SetPlayerCamera()
    {
        TurretCamera.Priority = -1;
        MainCamera.Priority = 10;
    }

    private void TurretController()
    {
        playerMesh.SetActive(false);
        FakePlayer.SetActive(true);
        playerAnimator.SetBool("Turret", true);
        playerPhysicController.enabled = false;
        playerManager.enabled = false;
        playerManager.playerRb.velocity = Vector3.zero;
        float horizontal = floatingJoystick.Horizontal;
        float vertical = floatingJoystick.Vertical;
        SetTurretCamera();
        //todo turret'a sınır konmadı valla
        minigunTurret.transform.Rotate(Vector3.forward, horizontal * 100f * Time.deltaTime);

        if (vertical < -0.75f)
        {
            SetPlayerCamera();
            PlayerController();
        }
    }
    private void PlayerController()
    {
        playerMesh.SetActive(true);
        FakePlayer.SetActive(false);
        playerAnimator.SetBool("Turret", false);
        playerPhysicController.enabled = true;
        playerManager.enabled = true;
    }
}
