using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class PlayerStackSignals :MonoBehaviour
    {
        #region Singleton

        public static PlayerStackSignals Instance;


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

        public UnityAction MoneyLeaveSignal = delegate { };
        public UnityAction InsideBase = delegate { };
        public UnityAction OutBase = delegate { };
        public UnityAction DoorControllerSignal = delegate { };
    }
}