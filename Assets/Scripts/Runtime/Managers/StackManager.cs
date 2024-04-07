using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class StackManager : MonoBehaviour
{
    
    private List<GameObject> moneyList = new List<GameObject>();
    private List<GameObject> ammoList = new List<GameObject>();
    private int _moneyListIndexCounter = 0;
    private Vector3 _firstMoneyPosition;
    private Vector3 _firstAmmoPosition;
    private Vector3 _currentMoneyPosition;
    private Vector3 _currentAmmoPosition;

    [SerializeField] private GameObject stackPlace;
    [SerializeField] private GameObject ammoStackPlace;
    [SerializeField] private GameObject AmmoPrefab;
    [SerializeField] private Transform ammoTransform;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float stackSpacing = 0.1f; 
    [SerializeField] private int maxMoneyCount = 20; 
    [SerializeField] private int maxAmmoCount = 4; 

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Money") && moneyList.Count < maxMoneyCount)
        {
            MoneyStack(other);
        }
        if(other.CompareTag("MoneyBullet"))
        {
            MoneyLeaving();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AmmoArea") && ammoList.Count < maxAmmoCount)
        {
            AmmoStack();
        }
    }

    private void AmmoStack()
    {
        var obj = Instantiate(AmmoPrefab, ammoTransform.position, Quaternion.identity);
        ammoList.Add(obj);
    
        if (ammoList.Count == 1)
        {
            _firstAmmoPosition = ammoStackPlace.transform.position;
            obj.transform.DOMove(_firstAmmoPosition, 1f)
                .SetEase(Ease.InOutElastic).OnComplete(() => 
                {
                    obj.transform.SetParent(ammoStackPlace.transform);
                    _currentAmmoPosition = _firstAmmoPosition;
                });
        }
        else
        {
           
            float newYPosition = _currentAmmoPosition.y + 1f;
            print(newYPosition);
            Vector3 targetPosition = new Vector3(_firstAmmoPosition.x, newYPosition, _firstAmmoPosition.z);
            obj.transform.DOMove(targetPosition, 1.5f)
                .SetEase(Ease.InOutElastic);
            obj.transform.SetParent(ammoStackPlace.transform);
            
            _currentAmmoPosition = targetPosition;
        }
    }




    private void MoneyStack(Collider other)
    {
        moneyList.Add(other.gameObject);
        if(moneyList.Count == 1)
        {
            _firstMoneyPosition = stackPlace.transform.position;

            other.gameObject.transform.DOMoveY(_firstMoneyPosition.y, 0.2f)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() => 
                {
                    other.gameObject.transform.rotation = stackPlace.transform.rotation;
                    _currentMoneyPosition = new Vector3(other.transform.position.x, stackPlace.transform.position.y , other.transform.position.z);
                    other.gameObject.GetComponent<StackController>().UpdateMoneyPosition(stackPlace.transform, true);
                });
        }
        else if(moneyList.Count > 1)
        {
              
            other.gameObject.transform.rotation = moneyList[moneyList.Count - 2].transform.rotation;
            other.gameObject.transform.DOMoveY(_currentMoneyPosition.y + stackSpacing, 0.2f)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() =>
                {
                    _currentMoneyPosition = new Vector3(other.transform.position.x, _currentMoneyPosition.y + stackSpacing, other.transform.position.z);
                    other.gameObject.GetComponent<StackController>().UpdateMoneyPosition(moneyList[_moneyListIndexCounter].transform, true);
                    _moneyListIndexCounter++;
                });
        }
    }

    private void MoneyLeaving()
    {
        for (int i = 0; i < moneyList.Count; i++)
        {
            var gameObj = moneyList[i];
            gameObj.transform.DOMove(stackPlace.transform.position, 0.2f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Destroy(gameObj);
                    uiManager.UpdateMoney(10);
                    moneyList.Clear();
                    _moneyListIndexCounter = 0;
                });
        }
    }
}
