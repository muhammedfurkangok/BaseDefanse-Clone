using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] private Transform[] _workerWayPoints;
    [SerializeField] private NavMeshAgent _workerNavMeshAgent;
    [SerializeField] private Transform _ammoPlace;
    [SerializeField] private GameObject AmmoPrefab;
    [SerializeField] private GameObject ammoStackPlace;

    private List<GameObject> ammoList = new List<GameObject>();
    private int maxAmmoCount = 4;
    private List<Vector3> _bulletLocation;
    

    private void Start()
    {
        GoToAmmoPlace();
    }

    private void GoToAmmoPlace()
    {
        _workerNavMeshAgent.SetDestination(_ammoPlace.position);
    }

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

    private void Update()
    {
        if (ammoList.Count < maxAmmoCount && _workerNavMeshAgent.remainingDistance <= 0.1f)
        {
            AmmoStack();
        }
        else if (_workerNavMeshAgent.remainingDistance <= 0.1f && ammoList.Count == maxAmmoCount)
        {
          _workerNavMeshAgent.SetDestination(_workerWayPoints[0].position);
          if(_workerNavMeshAgent.remainingDistance <= 0.01f)
          {
              MoveAmmoToTarget();
              _workerNavMeshAgent.SetDestination(_ammoPlace.position);
          }
        }
    }

  

    private void AmmoStack()
    {
        var obj = Instantiate(AmmoPrefab, ammoStackPlace.transform.position, ammoStackPlace.transform.rotation);
        ammoList.Add(obj);
        var _firstAmmoPosition = ammoStackPlace.transform.position;

        float newYPosition = _firstAmmoPosition.y + ammoList.Count - 1;

        obj.transform.DOMove(new Vector3(_firstAmmoPosition.x, newYPosition, _firstAmmoPosition.z), 0.01f)
            .SetEase(Ease.InOutElastic)
            .OnComplete(() =>
            {
                obj.transform.SetParent(ammoStackPlace.transform);
            });
    }

    private void MoveAmmoToTarget()
    {
        
        for (int i = 0; i < ammoList.Count; i++)
        {
            var obj = ammoList[i];
            obj.transform.DOMove(_workerWayPoints[0].transform.position + _bulletLocation[i], 0.2f)
                .SetEase(Ease.InOutElastic)
                .onComplete += () =>
            {
                obj.transform.SetParent(_workerWayPoints[0].transform);
                obj.transform.rotation = Quaternion.identity;
            };
        }
        ammoList.Clear();

    }
    
}
