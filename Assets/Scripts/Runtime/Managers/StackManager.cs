using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class StackManager : MonoBehaviour
{
     [ShowInInspector] public List<GameObject> moneyList = new List<GameObject>();
    private int _moneyListIndexCounter = 0;
    private Vector3 _firstMoneyPosition;
    private Vector3 _currentMoneyPosition;

    [SerializeField] private GameObject stackPlace;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float stackSpacing = 0.1f; 
    [SerializeField] private int maxMoneyCount = 20; 

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
