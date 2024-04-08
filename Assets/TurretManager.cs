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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            {
                TurretController();
                
                SetTurretCamera();
               
                StartCoroutine(turretAmmoManager.ShootBullets());
            }
        }
    }

    private void SetTurretCamera()
    {
        MainCamera.Priority = 0;
        TurretCamera.Priority = 1;
    }
    private void SetPlayerCamera()
    {
        TurretCamera.Priority = 0;
        MainCamera.Priority = 1;
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
        float currentRotationZ = minigunTurret.transform.eulerAngles.z;
        //todo turret'a sınır konmadı valla
        minigunTurret.transform.Rotate(Vector3.forward, horizontal * 100f * Time.deltaTime);
     
        
        
        if(vertical < -0.75f)
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
