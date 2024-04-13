using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Signals;
using UnityEngine;
using UnityEngine.AI;

public class HostageManager : MonoBehaviour
{

    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Transform _player;
    [SerializeField] private Animator _hostageAnimator;
    [SerializeField] private NavMeshAgent _hostageNavMeshAgent;
    [SerializeField] private GameObject _hostageHelpText;

    #endregion

    #region Private Variables

    private bool _isHostage = true;

    #endregion

    #region Public Variables
    
 

    #endregion

    #endregion

    private void Update()
    {
        if (!_isHostage)
        {
            _hostageNavMeshAgent.SetDestination(_player.position);
            if(_hostageNavMeshAgent.remainingDistance <= .5f)
            {
                _hostageAnimator.SetBool("Running", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (_isHostage)
        {
            if(other.CompareTag("Player"))
            {
                HostageSignals.Instance.HostageAdd.Invoke(gameObject);
           
                _isHostage = false;
                _hostageAnimator.SetBool("Running", true);
                _hostageHelpText.SetActive(false);
            }
        }
    }
}
