using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class MinerManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _minerNavMeshAgent;
    [SerializeField] private Animator _minerAnimator;
    public Transform[] _minerWayPoints;
     public Transform diamondPlace;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Transform gemInstantiatePosition;
    [SerializeField] private GameObject minerPickaxe;
    [SerializeField] private GameObject minerFakePickaxe;
    
    private readonly List<GameObject> _diamondList = new List<GameObject>();
    private List<Vector3> _diamondLocation;
    private float _directY;
    private float _directZ;
    private float _directX;
    private float LimitX = 5;
    private float LimitZ = 4;
    private float OffsetX = 0.3f;
    private float OffsetY = 0.2f;
    private float OffsetZ = 0.28f;

    private void Start()
    {  
        diamondPlace = GameObject.Find("MineDiamondArea").transform;
        Vector3 randomWayPointPosition = GameObject.Find("MinerWayPoint").transform.GetChild(Random.Range(0, GameObject.Find("MinerWayPoint").transform.childCount)).position;
        StartCoroutine(MinerBehaviour(randomWayPointPosition));
    }

    private IEnumerator MinerBehaviour(Vector3 destination)
    {
        while (true)
        {
            minerFakePickaxe.SetActive(true);
            _minerNavMeshAgent.SetDestination(destination);
            yield return new WaitUntil(() => _minerNavMeshAgent.remainingDistance <= 0.01f && !_minerNavMeshAgent.pathPending);
            minerFakePickaxe.SetActive(false);
            minerPickaxe.SetActive(true);
            _minerAnimator.SetBool("Digging", true);
            yield return new WaitForSeconds(5);
            minerPickaxe.SetActive(false);
            minerFakePickaxe.SetActive(true);
            _minerAnimator.SetBool("Digging", false);

            
            var obj =   Instantiate(gemPrefab, gemInstantiatePosition.position, gemPrefab.transform.rotation);
            _diamondList.Add(obj);
            obj.transform.SetParent(gemInstantiatePosition);
            _minerNavMeshAgent.SetDestination(diamondPlace.position);
          
            yield return new WaitUntil(() => _minerNavMeshAgent.remainingDistance <= 0.01f && !_minerNavMeshAgent.pathPending);
            
            obj.transform.SetParent(diamondPlace);
            SetGemPos(obj);
            obj.transform.rotation = Quaternion.Euler(180f,0f,0f);
            obj.transform.localScale = new Vector3(1f,1f,1f);
           
            yield return new WaitForSeconds(1);
            
        }
    }

    private void SetGemPos(GameObject obj)
    {
        _directX = diamondPlace.childCount % LimitX * OffsetX;
        _directY = diamondPlace.childCount / (LimitX * LimitZ) * OffsetY;
        _directZ = diamondPlace.childCount % (LimitX * LimitZ) / LimitX * OffsetZ;
        obj.transform.DOLocalMove(new Vector3(_directX, _directY, _directZ), 0.5f);
    }
}
