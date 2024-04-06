using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseOutTrigger : MonoBehaviour
{
    #region Self Variables

    #region Serialized Veriables

    [SerializeField] private PlayerAnimationController _playerAnimationController;

    #endregion

    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerAnimationController.playerStates = PlayerStates.Fight;
        }
    }
}
