using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables

    #region Public Variables

    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;
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
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private Transform moneyDropPoint;
    [SerializeField] private GameObject enemyHand;

    #endregion

    #region Private Variables

    private Vector3 nextPosition;
    private bool hasAttacked = false;

    #endregion

    #endregion

    private void Awake()
    {
        player = GameObject.Find("PlayerManager").transform;
        playerAnimationController = GameObject.Find("Mesh").GetComponent<PlayerAnimationController>();
        agent = GetComponent<NavMeshAgent>();
        Transform randomChild = GameObject.Find("hitPlaces").transform.GetChild(Random.Range(0, GameObject.Find("hitPlaces").transform.childCount));
        nextPosition = randomChild.position;
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



    private void AttackPlayer()
    {
        enemyAnimator.SetBool("Attack",true);
    }
    


    private void ChasePlayer()
    {
        
        enemyAnimator.SetBool("Attack",false);
        agent.SetDestination(player.position);
        hasAttacked = false;
     
    }

    private void Patroling()
    {
        agent.SetDestination(nextPosition);
        if (!agent.pathPending  && agent.remainingDistance < 6f)
        {
            enemyAnimator.SetBool("Attack",true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            bloodParticle.Play();
            enemyHealth -= 50;
         
            if (enemyHealth <= 0)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                DropMoney();
                agent.isStopped = true;
                enemyAnimator.SetBool("Attack",false);
                enemyAnimator.SetBool("Die",true);
                Destroy(gameObject,2f);
               
            }
        }
       else if (other.CompareTag("TurretBullet"))
        {
            enemyHealth -= 100;
            //todo:particle
            agent.isStopped = true;
            enemyAnimator.SetBool("Attack",false);
            enemyAnimator.SetTrigger("Die");
            Destroy(gameObject,2f);
        }
    } 
    private void DropMoney()
    {
        //todo flip money 
        GameObject money = Instantiate(moneyPrefab, moneyDropPoint.position, moneyPrefab.transform.rotation);
        money.transform.DOJump(new Vector3(money.transform.position.x, 0f, money.transform.position.z), 1f, 1, 1f);
    }
}
