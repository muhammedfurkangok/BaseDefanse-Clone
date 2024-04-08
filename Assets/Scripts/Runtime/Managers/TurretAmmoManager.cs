using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class TurretAmmoManager : MonoBehaviour
{
    [SerializeField] private GameObject ammoPlace;
    [SerializeField] private GameObject turretMuzzle;
    [SerializeField] private GameObject ammoPrefab;
    private float bulletDelay = 0.3f; // Her mermi arasındaki bekleme süresi

    private void TurretShoot()
    {
        var obj = Instantiate(ammoPrefab, turretMuzzle.transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(turretMuzzle.transform.forward * 1000f);
        Destroy(obj, 1f);
    }

    [Button]
    private void UseAmmo()
    {
        StartCoroutine(ShootBullets());
    }

    private IEnumerator ShootBullets()
    {
        foreach (Transform child in ammoPlace.transform)
        {
            for (int i = 0; i < 4; i++) // Her bir child için 4 mermi at
            {
                TurretShoot();
                yield return new WaitForSeconds(bulletDelay);
            }

            Destroy(child.gameObject); // Her bir child'i yok et
        }

        if (ammoPlace.transform.childCount == 0)
        {
            Debug.Log("No Ammo");
        }
    }
}