using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;

    [SerializeField] private bool playerInSightRange;
    private float _fireRate = 1f; 
    private float _nextFireTime = 0f; 
    void Update()
    {
        if (Time.time > _nextFireTime)
        {
            Fire();
            _nextFireTime = Time.time + _fireRate;
        }
        
    }
    
    private void Fire()
    {
       
        GameObject bullet = Instantiate(_bulletPrefab, _gunBarrel.position, _gunBarrel.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = _gunBarrel.forward * _bulletSpeed;
        Destroy(bullet, _bulletLifeTime);
    }
}
