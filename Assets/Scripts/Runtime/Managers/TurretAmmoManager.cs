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
    private float bulletDelay = 0.4f; 

    private void TurretShoot()
    {
        var obj = Instantiate(ammoPrefab, turretMuzzle.transform.position, rotationMuzzle.transform.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce(turretMuzzle.transform.forward * 1000f); 
        Destroy(obj, 1f);
    }
    
    public async UniTask ShootBullets()
    {
        if (ammoPlace.transform.childCount == 0)
        {
           return;
        }
        else
        {
            int childCount = ammoPlace.transform.childCount; 
            for (int i = 0; i < childCount; i++)
            {
                GameObject childObject = ammoPlace.transform.GetChild(i).gameObject;
                
                for (int j = 0; j < 4; j++) 
                {
                    TurretShoot();
                    await UniTask.Delay(200);
                }
                Destroy(childObject);
            }
        }
    }
}