using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageManager : MonoBehaviour
{

    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Transform _player;
    [SerializeField] private Animator _hostageAnimator;

    #endregion

    #region Private Variables

    private bool _isHostage = true;

    #endregion

    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isHostage = false;
        }
    }
}
