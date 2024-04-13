using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GemAreaController : MonoBehaviour
{
   #region Serialized Variables

   [SerializeField] private UIManager uiManager;
   [SerializeField] private GameObject gemStackPlace;
   [SerializeField] private GameObject jumpPlace;
   #endregion

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
        
         for (int i = gemStackPlace.transform.childCount - 1; i >= 0; i--)
         {
            var obj = gemStackPlace.transform.GetChild(i).gameObject; // gameObject özelliği alındı
            obj.transform.DOJump(other.transform.position, 1f, 1, 0.2f).SetEase(Ease.OutQuad)
               .OnComplete(() =>
               {
                  Destroy(obj);
                  uiManager.UpdateGem(25);
               });
         }
      }
   }

}
