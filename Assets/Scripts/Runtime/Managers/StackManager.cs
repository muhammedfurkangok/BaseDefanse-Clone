using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private List<GameObject> moneyList = new List<GameObject>();
    private int _moneyListIndexCounter = 0;

    private Vector3 _firstMoneyPosition;
    private Vector3 _currentMoneyPosition;
   
    
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private GameObject stackPlace;


    private void OnTriggerEnter(Collider money)
    {
        if (money.CompareTag("Money"))
        {
            moneyList.Add(money.gameObject);
            if(moneyList.Count == 1)
            {
                
                _firstMoneyPosition = stackPlace.transform.position;
                
                _currentMoneyPosition = new Vector3(money.transform.position.x, _firstMoneyPosition.y, money.transform.position.z);
                
                money.gameObject.transform.position = _firstMoneyPosition;
                _currentMoneyPosition = new Vector3(money.transform.position.x,stackPlace.transform.position.y ,money.transform.position.z);
                money.gameObject.GetComponent<StackController>().UpdateMoneyPosition(stackPlace.transform, true);

            }
            else if(moneyList.Count > 1)
            {
                
                money.gameObject.transform.position = _currentMoneyPosition;
                _currentMoneyPosition = new Vector3(money.transform.position.x, _currentMoneyPosition.y + 0.3f,money.transform.position.z);
                money.gameObject.GetComponent<StackController>().UpdateMoneyPosition(moneyList[_moneyListIndexCounter].transform, true);
                _moneyListIndexCounter++;
            }
        }
    }
}
