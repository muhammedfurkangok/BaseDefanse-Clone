using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   internal int moneyCount = 10000;
   [SerializeField] private TextMeshProUGUI moneyText;
   public void UpdateMoney(int money)
   {
      moneyCount += money;
      moneyText.text = moneyCount.ToString();
     
   }
  
   public void PayMoney(int money)
   {
      if (moneyCount > 0)
      {
      moneyCount -= money;
      moneyText.text = moneyCount.ToString();
      }
   }
}
