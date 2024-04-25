using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Managers
{
    public class EnemyDamageManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player");
                PlayerHealthManager.Instance.TakeDamage(10);
            }
        }
    }
}