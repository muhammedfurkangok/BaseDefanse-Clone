using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    
    private int _damage = 50;
     private void OnTriggerEnter(Collider other)
     {
         EnemyManager enemyManager = other.GetComponent<EnemyManager>();
               if (other.CompareTag("Enemy"))
               {
                    enemyManager.enemyHealth -= _damage;
                    Destroy(gameObject , 2f);
                    if(enemyManager.enemyHealth <= 0)
                    {
                        enemyManager.enemyAnimator.SetTrigger("Die");
                        Destroy(other.gameObject,2f);
                    }
               }
     }
}
