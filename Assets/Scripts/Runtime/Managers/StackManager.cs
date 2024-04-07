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
    [SerializeField] private float stackSpacing = 0.1f; // Para nesneleri arasındaki mesafe
    [SerializeField] private int maxMoneyCount = 20; // Maksimum para sayısı

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MoneyBullet"))
        {
            MoneyLeaving();
        }
        if (other.CompareTag("Money") && moneyList.Count < maxMoneyCount)
        {
            moneyList.Add(other.gameObject);
            if(moneyList.Count == 1)
            {
                _firstMoneyPosition = stackPlace.transform.position;

                other.gameObject.transform.DOMoveY(_firstMoneyPosition.y + stackSpacing, 0.2f)
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
               Debug.Log("moneyList.Count: " + moneyList.Count);
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
    }
    public void MoneyLeaving()
    {
        var limit = moneyList.Count;
        for (var i = limit - 1; i >= 0; i--)
        {
            var obj = moneyList[0];
            moneyList.RemoveAt();
            moneyList.TrimExcess();
            obj.transform.DOLocalMove(
                new Vector3(Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f), Random.Range(-0.5f, 1f)), 0.5f);
            obj.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                uiManager.UpdateMoney(10);
                Destroy(obj);
            });
        }
    }

}
