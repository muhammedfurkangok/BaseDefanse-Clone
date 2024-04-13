using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class HostageSignals : MonoBehaviour
    {
        #region Singleton

        public static HostageSignals Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        public UnityAction<GameObject,GameObject> MinerHostageLeave = delegate { };
        public UnityAction<GameObject,GameObject> SoliderHostageLeave = delegate { };
        public UnityAction<GameObject> HostageAdd = delegate { };
    }
}