using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private bool playerInSightRange;
    private float _fireRate = 1f; 
    private float _nextFireTime = 0f; 
   // Görüş menzili
    
    void Update()
    {
        if (Time.time > _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + _fireRate;
        }
        // Gizmo alanında düşmanı algılama
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Düşmanın olduğu yöne dönme işlemi
                Vector3 direction = collider.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
                break; // Sadece bir düşmanı hedef al
            }
        }
    }
        
    


    private void Fire()
    {
       
        GameObject bullet = Instantiate(_bulletPrefab, _gunBarrel.position, _gunBarrel.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = _gunBarrel.forward * _bulletSpeed;
        Destroy(bullet, _bulletLifeTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
