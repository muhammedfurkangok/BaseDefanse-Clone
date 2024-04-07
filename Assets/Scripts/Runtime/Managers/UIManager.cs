using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
   private int moneyCount = 0;
   public void UpdateMoney(int money)
   {
      moneyCount += money;
      Debug.Log("Money Count: " + moneyCount);
   }
  
}
