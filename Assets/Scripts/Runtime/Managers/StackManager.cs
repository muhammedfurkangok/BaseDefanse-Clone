using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private List<GameObject> moneyList = new List<GameObject>();
    private int _moneyListIndexCounter = 0;

    private Vector3 _firstMoneyPosition;
    private Vector3 _currentMoneyPosition;

    [SerializeField] private GameObject stackPlace;
    [SerializeField] private float stackSpacing = 0.1f; // Para nesneleri arasındaki mesafe
    [SerializeField] private int maxMoneyCount = 20; // Maksimum para sayısı

    private void OnTriggerEnter(Collider other)
    {
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
}
