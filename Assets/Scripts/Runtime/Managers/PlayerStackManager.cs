using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Signals;
using UnityEngine;

public class PlayerStackManager : MonoBehaviour
{
   [SerializeField] private StackManager stackManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            PlayerStackSignals.Instance.DoorControllerSignal?.Invoke();
         
        }
    }

  
}
