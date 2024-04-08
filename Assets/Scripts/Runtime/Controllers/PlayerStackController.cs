using System;
using DG.Tweening;
using Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Controllers
{
   

    public class PlayerStackController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
  
        [SerializeField] private Transform Door;
        [SerializeField] private StackManager stackManager;
        [SerializeField] private UIManager uiManager;

        #endregion

        #region Private Variables
        #endregion

        #endregion

        private void Start()
        {
      
            PlayerStackSignals.Instance.BulletStackLeaveSignal += BulletStackLeave;
            PlayerStackSignals.Instance.DoorControllerSignal += DoorController;
        }

        


        private void BulletStackLeave()
        {
            
        }
      
        private void DoorController()
        {
           
            Door.DOLocalRotate(new Vector3(0, 0, 90), 1f)
                .SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    Door.DOLocalRotate(new Vector3(0, 0, 0), 2f)
                        .SetEase(Ease.InOutFlash);
                });
            
        }
        private void OnDisable()
        {
            
            PlayerStackSignals.Instance.BulletStackLeaveSignal -= BulletStackLeave;
            PlayerStackSignals.Instance.DoorControllerSignal -= DoorController;
        }
    }
    
}