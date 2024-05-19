using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Controllers;
using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

public class HostageTrigger : MonoBehaviour
{
    [SerializeField] private HostageController hostageController;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            for(int i = 0; i< hostageController.hostageList.Count ; i++)
            {
                HostageSignals.Instance.MinerHostageLeave.Invoke(hostageController.hostageList[i], hostageController.hostageList[i]);
               
            }
        }
    }
}
