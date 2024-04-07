using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   private int moneyCount = 0;
   [SerializeField] private TextMeshProUGUI moneyText;
   public void UpdateMoney(int money)
   {
      moneyCount += money;
      moneyText.text = moneyCount.ToString();
     
   }
  
}
