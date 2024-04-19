using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class StackManager : MonoBehaviour
{

    private List<GameObject> moneyList = new List<GameObject>();
    private List<GameObject> ammoList = new List<GameObject>();
    [SerializeField] private GameObject[] paths;
    private int _moneyListIndexCounter = 0;
    private Vector3 _firstMoneyPosition;
    private Vector3 _firstAmmoPosition;
    private Vector3 _currentMoneyPosition;
    private Vector3 _currentAmmoPosition;
    public List<Vector3> _bulletLocation;

    [SerializeField] private GameObject stackPlace;
    [SerializeField] private GameObject ammoStackPlace;
    [SerializeField] private GameObject AmmoPrefab;
    [SerializeField] private GameObject turretStackPlace;
    [SerializeField] private Transform ammoTransform;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float stackSpacing = 0.1f;
    [SerializeField] private int maxMoneyCount = 20;
    [SerializeField] private int maxAmmoCount = 6;

    private void Awake()
    {
        _bulletLocation = new List<Vector3>
        {
            new Vector3(-1f, 0, 0.5f),
            new Vector3(0f, 0, 0.5f),
            new Vector3(0f, 0, -0.5f),
            new Vector3(-1f, 0, -0.5f)
        };
    }

    public void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Money") && moneyList.Count < maxMoneyCount)
        {
            MoneyStack(other);
        }
        else if (other.CompareTag("MoneyBullet"))
        {
            MoneyLeaving();
        }
        else if (other.CompareTag("TurretStack"))
        {
                for (int i = 0; i < ammoList.Count; i++)
                {
                    var obj = ammoList[i];
                    obj.transform.DOMove(turretStackPlace.transform.position + _bulletLocation[i], 0.2f).SetEase(Ease.InOutElastic).onComplete += () =>
                    {
                        obj.transform.SetParent(turretStackPlace.transform);
                        obj.transform.rotation = Quaternion.identity;
                       
                    };
                }
                ammoList.Clear();
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
            .OnComplete(() => { obj.transform.SetParent(ammoStackPlace.transform); });
    }

    private void MoneyStack(Collider other)
    {
        moneyList.Add(other.gameObject);
        if (moneyList.Count == 1)
        {
            _firstMoneyPosition = stackPlace.transform.position;

            other.gameObject.transform.DOMoveY(_firstMoneyPosition.y, 0.2f)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() =>
                {
                    other.gameObject.transform.SetParent(stackPlace.transform);
                    other.gameObject.transform.rotation = stackPlace.transform.rotation;
                    _currentMoneyPosition = new Vector3(other.transform.position.x, stackPlace.transform.position.y,
                        other.transform.position.z);
                    other.gameObject.GetComponent<StackController>().UpdateMoneyPosition(stackPlace.transform, true);
                });
        }
        else if (moneyList.Count > 1)
        {

          
            other.gameObject.transform.DOMoveY(_currentMoneyPosition.y + stackSpacing, 0.2f)
                .SetEase(Ease.InOutElastic)
                .OnComplete(() =>
                {
                    Quaternion targetRotation = moneyList[moneyList.Count - 2].transform.rotation;
                    Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
                    other.gameObject.transform.rotation = targetRotation;
                    other.gameObject.transform.SetParent(stackPlace.transform);
                    _currentMoneyPosition = new Vector3(other.transform.position.x,
                        _currentMoneyPosition.y + stackSpacing, other.transform.position.z);
                    other.gameObject.GetComponent<StackController>()
                        .UpdateMoneyPosition(moneyList[_moneyListIndexCounter].transform, true);
                    _moneyListIndexCounter++;
                });
        }
    }

    private void MoneyLeaving()
    {
        foreach (var gameObj in moneyList)
        {
            Vector3 randomPoint = paths[UnityEngine.Random.Range(0, paths.Length)].transform.position;
            Vector3[] pathPoints = new Vector3[] { randomPoint, stackPlace.transform.position };
            gameObj.transform.DOPath(pathPoints, 0.2f, PathType.CatmullRom)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Destroy(gameObj);
                });
        }
        uiManager.UpdateMoney(10);
        moneyList.Clear();
        _moneyListIndexCounter = 0;
    }
}

