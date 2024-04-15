using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class EnemySignals : MonoBehaviour
    {
        #region Singleton

        public static EnemySignals Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
        
        private UnityAction EnemySpawned = delegate {  };
        private UnityAction EnemyDied = delegate {  };
    }
}