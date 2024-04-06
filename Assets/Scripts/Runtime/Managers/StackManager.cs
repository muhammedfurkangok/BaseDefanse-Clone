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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            if (other.CompareTag("Money"))
            {
                moneyList.Add(other.gameObject);
                if(moneyList.Count == 1)
                {
                    //TODO lerp mekaniğinin düzeltilmesi gerekiyor!!!!!!!!
                    other.gameObject.transform.SetParent(stackPlace.transform);
                    _firstMoneyPosition = stackPlace.transform.position.y * Vector3.up;
        
                    _currentMoneyPosition = new Vector3(other.transform.position.x, _firstMoneyPosition.y, other.transform.position.z);

                    other.gameObject.transform.position = _firstMoneyPosition;
                    moneyList[0].transform.rotation = transform.rotation;
                    other.gameObject.transform.rotation = moneyList[0].transform.rotation ; // İlk elemanın rotasyonunu al
                    _currentMoneyPosition = new Vector3(other.transform.position.x, stackPlace.transform.position.y + 0.45f , other.transform.position.z);
                    other.gameObject.GetComponent<StackController>().UpdateMoneyPosition(stackPlace.transform, true);

                }
                else if(moneyList.Count > 1)
                {
                    other.gameObject.transform.SetParent(stackPlace.transform);
                    other.gameObject.transform.position = _currentMoneyPosition;
                    other.gameObject.transform.rotation = moneyList[moneyList.Count - 2].transform.rotation; // Önceki paranın rotasyonunu al
                    _currentMoneyPosition = new Vector3(other.transform.position.x, _currentMoneyPosition.y + 0.45f, other.transform.position.z);
                    other.gameObject.GetComponent<StackController>().UpdateMoneyPosition(moneyList[_moneyListIndexCounter].transform, true);
                    _moneyListIndexCounter++;
                }
            }
        }
    }
}
