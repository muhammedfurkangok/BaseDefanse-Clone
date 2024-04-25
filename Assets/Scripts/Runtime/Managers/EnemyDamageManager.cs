using UnityEngine;

namespace Runtime.Managers
{
    public class EnemyDamageManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHealthManager.Instance.TakeDamage(10);
            }
        }
    }
}