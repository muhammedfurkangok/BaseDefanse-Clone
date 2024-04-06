using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   #region Self Variables

   #region Serialized Variables

   [SerializeField] private GameObject SettingsSlider;

   #endregion

   #endregion

   public void SettingsEnableDisable()
   {
     if(SettingsSlider.activeSelf)
     {
         SettingsSlider.SetActive(false);
     }
     else
     {
         SettingsSlider.SetActive(true);
     }
      
   }
}
