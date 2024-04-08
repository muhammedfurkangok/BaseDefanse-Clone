using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    [SerializeField] private GameObject turretStackPlace;
    [SerializeField] private Transform ammoTransform;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float stackSpacing = 0.1f; 
    [SerializeField] private int maxMoneyCount = 20; 
    [SerializeField] private int maxAmmoCount = 4;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 1f))
        {
            if (hit.collider.CompareTag("TurretStack"))
            {
                int cornerIndex = 0;
                float cornerOffset = 0.5f; // Offset from the corner to place the object

                // Calculate a consistent Y level for all objects
                float yPos = hit.transform.position.y;

                // Iterate through each object in the ammoList
                for (int index = 0; index < ammoList.Count; index++)
                {
                    // Calculate the position for the current object at one of the four corners
                    Vector3 cornerPosition = hit.transform.position;
                    switch (cornerIndex)
                    {
                        case 0: // Top-left corner
                            cornerPosition += new Vector3(-cornerOffset, yPos, cornerOffset);
                            break;
                        case 1: // Top-right corner
                            cornerPosition += new Vector3(cornerOffset, yPos, cornerOffset);
                            break;
                        case 2: // Bottom-left corner
                            cornerPosition += new Vector3(-cornerOffset, yPos, -cornerOffset);
                            break;
                        case 3: // Bottom-right corner
                            cornerPosition += new Vector3(cornerOffset, yPos, -cornerOffset);
                            break;
                    }

                    var gameObj = ammoList[index];
                    gameObj.transform.DOMove(cornerPosition, 0.5f).SetEase(Ease.InOutElastic).OnComplete(() =>
                    {
                        gameObj.transform.SetParent(hit.transform);
                        ammoList.Clear();
                    });
                    gameObj.transform.DORotate(Vector3.zero, 0.5f);

                    // Increment corner index (cycle through the four corners)
                    cornerIndex = (cornerIndex + 1) % 4;
                }
            }
        }
    }



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
        var obj = Instantiate(AmmoPrefab, ammoTransform.position, ammoStackPlace.transform.rotation);
        ammoList.Add(obj);
        _firstAmmoPosition = ammoStackPlace.transform.position;
        
        float newYPosition = _firstAmmoPosition.y + ammoList.Count - 1;

        obj.transform.DOMove(new Vector3(_firstAmmoPosition.x, newYPosition, _firstAmmoPosition.z), 0.01f)
            .SetEase(Ease.InOutElastic)
            .OnComplete(() => 
            {
                obj.transform.SetParent(ammoStackPlace.transform);
            });
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
    private void BulletLeaving()
    {
        for (int i = 0; i < ammoList.Count; i++)
        {
            var gameObj = ammoList[i];
            gameObj.transform.DOMove(ammoStackPlace.transform.position, 0.2f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Destroy(gameObj);
                    ammoList.Clear();
                });
        }
    }
}
