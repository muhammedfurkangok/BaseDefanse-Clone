using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretAmmoManager : MonoBehaviour
{
    [SerializeField] private GameObject ammoPlace;
    [SerializeField] private GameObject rotationMuzzle;
    [SerializeField] private GameObject turretMuzzle;
    [SerializeField] private GameObject ammoPrefab;
    private float bulletDelay = 0.1f; // Her mermi arasındaki bekleme süresi

    private void TurretShoot()
    {
        var obj = Instantiate(ammoPrefab, turretMuzzle.transform.position, rotationMuzzle.transform.rotation);
        obj.GetComponent<Rigidbody>().AddForce(turretMuzzle.transform.forward * 1000f);
        Destroy(obj, 1f);
    }
    
    public IEnumerator ShootBullets()
    {
        foreach (Transform child in ammoPlace.transform)
        {
            
            TurretShoot();
            yield return new WaitForSeconds(bulletDelay);
            Destroy(child.gameObject);
             // Her bir child'i yok et
        }

        if (ammoPlace.transform.childCount == 0)
        {
            Debug.Log("No Ammo");
        }
    }
}