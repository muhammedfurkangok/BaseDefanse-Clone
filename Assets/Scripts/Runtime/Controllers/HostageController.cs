using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers
{
    public class HostageController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private HostageStates hostageStates;
        
        #endregion

        #region Public Variables

        [NonSerialized] public List<GameObject> hostageList = new List<GameObject>();
        [NonSerialized] public List<GameObject> minerList = new List<GameObject>();
        [NonSerialized] public List<GameObject> soliderList = new List<GameObject>();

        #endregion
        
        #endregion

        private void Start()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            HostageSignals.Instance.HostageAdd += HostageAdd;
            HostageSignals.Instance.MinerHostageLeave += MinerHostageLeave;
            HostageSignals.Instance.SoliderHostageLeave += SoliderHostageLeave;
        }

        private void SoliderHostageLeave(GameObject hostage, GameObject solider)
        {
            //text++
            if (hostageList.Contains(hostage))
            {
                hostageList.Remove(hostage);
                soliderList.Add(solider);
                Debug.Log("Hostage moved to solider list.");
            }
            else
            {
                Debug.LogWarning("Hostage not found in the hostage list.");
            }
        }

        private void MinerHostageLeave(GameObject hostage, GameObject miner)
        {   
            //text++
            if (hostageList.Contains(hostage))
            {
                hostageList.Remove(hostage);
                minerList.Add(miner);
            }
            else
            {
                Debug.LogWarning("Hostage not found in the hostage list.");
            }
        }
        private void HostageAdd(GameObject hostage)
        {
            hostageList.Add(hostage);
        }
        
        private void UnSubscribeEvents()
        {
            HostageSignals.Instance.HostageAdd -= HostageAdd;
            HostageSignals.Instance.MinerHostageLeave -= MinerHostageLeave;
            HostageSignals.Instance.SoliderHostageLeave -= SoliderHostageLeave;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

    }
}