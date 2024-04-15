// TurretAmmoManager.cs
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TurretAmmoManager : MonoBehaviour
{
    [SerializeField] private GameObject ammoPlace;
    [SerializeField] private GameObject rotationMuzzle;
    [SerializeField] private GameObject turretMuzzle;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private TurretManager turretManager;
    [SerializeField] private float bulletDelay = 0.1f; 

    private void TurretShoot()
    {
        var obj = Instantiate(ammoPrefab, turretMuzzle.transform.position, rotationMuzzle.transform.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(turretMuzzle.transform.forward * 1000f); 
        Destroy(obj, 1f);
    }
    
    public async UniTask ShootBullets()
    {
        while (ammoPlace.transform.childCount > 0)
        {
            GameObject childObject = ammoPlace.transform.GetChild(0).gameObject;
        
            for (int j = 0; j < 4; j++) 
            {
                TurretShoot();
                await UniTask.WaitForSeconds(bulletDelay);
                if(turretManager.isTurretExit)
                {
                    turretManager.isTurretExit = false;
                }
            }
        
            Destroy(childObject);
        }
    }
    public bool HasAmmo()
    {
        return ammoPlace.transform.childCount > 0;
    }
}