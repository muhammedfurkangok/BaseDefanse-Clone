using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Managers;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField] internal int moneyCount = 0;
   [SerializeField] internal int gemCount = 0;
   [SerializeField] private TextMeshProUGUI moneyText;
   [SerializeField] private TextMeshProUGUI gemText;
   [SerializeField] private GameObject _healthBar;
   [SerializeField] private Image _healthSlider;
   [SerializeField] private TextMeshProUGUI _healthText;
   [SerializeField] private PlayerAnimationController _playerAnimationController;

   private void Start()
   {
      SubscribeEvents();
      onHealthChanged(PlayerHealthManager.Instance.health);
   }

   private void SubscribeEvents()
   {
      PlayerStackSignals.Instance.OutBase += OutBase;
      PlayerStackSignals.Instance.InsideBase += InsideBase;
      PlayerHealthManager.Instance.OnHealthChanged += onHealthChanged;
   }

   private void onHealthChanged(int arg0)
   {
      _healthSlider.fillAmount = arg0 / 100f ;
      _healthText.text = arg0.ToString() ;
   }

   private void InsideBase()
   {
      _healthBar.SetActive(false);
   }

   private void OutBase()
   {
      _healthBar.SetActive(true);
   }

   private void UnSubscribeEvents()
   {
      PlayerStackSignals.Instance.OutBase -= OutBase;
      PlayerStackSignals.Instance.InsideBase -= InsideBase;
   }

   private void OnDisable()
   {
      UnSubscribeEvents();
   }

   public void UpdateMoney(int money)
   {
      moneyCount += money;
      moneyText.text = moneyCount.ToString();
     
   }

   public void UpdateGem(int gem)
   {
      gemCount += gem;
      gemText.text = gemCount.ToString();
   }
  
   public void PayMoney(int money)
   {
      if (moneyCount > 0)
      {
         moneyCount -= money;
         moneyText.text = moneyCount.ToString();
      }
   }
   public void PayGem(int gem)
   {
      if (gemCount > 0)
      {
         gemCount -= gem;
         gemText.text = gemCount.ToString();
      }
   }
}
