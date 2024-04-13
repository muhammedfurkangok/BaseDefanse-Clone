using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MinerManager : MonoBehaviour
{
   #region Self Variables

   #region Serialized Variables

   [SerializeField] private NavMeshAgent _minerNavMeshAgent;
   [SerializeField] private Transform[] _minerWayPoints;
   [SerializeField] private Transform diamondPlace;
   [SerializeField] private GameObject gemPrefab;
   [SerializeField] private Transform gemInstantiatePosition;
   #endregion

   #region Private Variables

   

   #endregion

   #endregion

   private void Start()
   {
      
      MinerBehaviour();
   }

   private async void MinerBehaviour()
   {

       while (true)
       {
           int randomWayPoint = Random.Range(0, _minerWayPoints.Length);
           Vector3 RandomWayPoint = _minerWayPoints[randomWayPoint].position;
           _minerNavMeshAgent.SetDestination(RandomWayPoint);
           
           if(_minerNavMeshAgent.remainingDistance <= 0.5f)
           {
               //Trigger mine animation
               //Look At
               await UniTask.WaitForSeconds(5);
               
               Instantiate(diamondPlace, gemInstantiatePosition.position, Quaternion.identity);
               _minerNavMeshAgent.SetDestination(diamondPlace.position);
               //Trigger drop animaiton
           }
       }
   }
}
