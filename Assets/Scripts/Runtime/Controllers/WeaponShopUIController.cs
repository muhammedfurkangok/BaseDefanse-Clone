using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShopUIController : MonoBehaviour
{
    [SerializeField] private GameObject weaponShopUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            weaponShopUI.SetActive(true);
        }
    }

    public void ButtonExit()
    {
        weaponShopUI.SetActive(false);
    }
}
