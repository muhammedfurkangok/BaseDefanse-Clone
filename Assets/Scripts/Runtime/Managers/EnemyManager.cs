using Cysharp.Threading.Tasks;
using Runtime.Controllers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public int enemyHealth = 100;

    #endregion

    #region Serialized Variables

    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] public Animator enemyAnimator;
    [SerializeField] private GameObject[] waypoints;

    #endregion

    #region Private Variables

    private Vector3 nextPosition;

    #endregion

    #endregion

    private void Awake()
    {
        player = GameObject.Find("PlayerManager").transform;
        playerAnimationController = GameObject.Find("Mesh").GetComponent<PlayerAnimationController>();
        agent = GetComponent<NavMeshAgent>();
        Transform randomChild = GameObject.Find("hitPlaces").transform.GetChild(Random.Range(0, GameObject.Find("hitPlaces").transform.childCount));
        nextPosition = randomChild.position;
       
        if(enemyHealth <= 0)
        {
            
            enemyAnimator.SetTrigger("Die");
            Destroy(gameObject, 2f);
        }
    }

    private void Update()
    { 
       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerAnimationController.playerStates == PlayerStates.Fight)
        {
             if (playerInSightRange && !playerInAttackRange) ChasePlayer();
             if (playerInSightRange && playerInAttackRange) AttackPlayer();
            
        }
        if (!playerInSightRange && !playerInAttackRange) Patroling();
    }



    private   void AttackPlayer()
    {
        enemyAnimator.SetTrigger("Attack");
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        enemyAnimator.SetTrigger("Walk");
    }

    private void Patroling()
    {
        agent.SetDestination(nextPosition);
        if (!agent.pathPending  && agent.remainingDistance < 6f)
        {
            enemyAnimator.SetTrigger("Attack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
