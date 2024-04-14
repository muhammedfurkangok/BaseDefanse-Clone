using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers
{
    public class HostageController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private HostageStates hostageStates;
        [SerializeField] private TextMeshPro minerText;
        [SerializeField] private TextMeshPro soliderText;
        
        #endregion

        #region Public Variables

        [NonSerialized] public List<GameObject> hostageList = new List<GameObject>();
        [NonSerialized] public List<GameObject> minerList = new List<GameObject>();
        [NonSerialized] public List<GameObject> soliderList = new List<GameObject>();

        #endregion

        #region Private Variables
        private int minerCount;
        private int soliderCount;

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
            soliderCount++;
            
            soliderText.text = soliderText.ToString()+"/5";
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
            minerCount++;
            minerText.text = minerCount.ToString() + "/5";
            if (hostageList.Contains(hostage))
            {
                hostageList.Remove(hostage);
                minerList.Add(miner);
                MinerManager minerManagerComponent = miner.GetComponent<MinerManager>();
                if (minerManagerComponent != null)
                {
                    minerManagerComponent.enabled = true;
                }
                HostageManager hostageManager = miner.GetComponent<HostageManager>();
                if (hostageManager != null)
                {
                    hostageManager.enabled = false;
                }
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