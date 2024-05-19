using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Managers
{

    public class PlayerHealthManager : MonoBehaviour
    {
         #region Singleton

         public static PlayerHealthManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            } }

        #endregion
        
        public UnityAction<int> OnPlayerHealthDecreased = delegate {  };
        public UnityAction<int> OnHealthChanged = delegate {  };
        public UnityAction OnPlayerDied = delegate {  };
        public int health = 100;
        public int maxHealth = 100;
        [SerializeField] private UIManager uiManager;
    
        
        public void TakeDamage(int damage)
        {
            health -= damage;
            health = Math.Clamp(health, 0, maxHealth);
            
            OnHealthChanged?.Invoke(health);
            
            if (health <= 0 )
            {
                OnPlayerDied.Invoke();
            }
        }
    }
}