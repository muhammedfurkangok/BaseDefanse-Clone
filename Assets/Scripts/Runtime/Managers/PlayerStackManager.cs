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
        stackManager.moneyList.Remove(other.gameObject);
        Debug.Log(stackManager.moneyList.Count);
        if (other.CompareTag("Player"))
        {   
            PlayerStackSignals.Instance.DoorControllerSignal?.Invoke();
           stackManager.MoneyLeaving();
        }
    }

  
}
