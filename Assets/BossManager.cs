using Cysharp.Threading.Tasks;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Serialized Variables
    
    [SerializeField] private LayerMask whatIsPlayer; 
    [SerializeField] private float attackRange; 
    [SerializeField] private float tackleRange; 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossHand;
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject grenade; 
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float forwardForce;
    [SerializeField] private float upForce;

    #endregion
    
    private bool isAttackRange;
    private bool isTackleRange;
    private bool alreadyAttacked;

    void Update()
    {
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isTackleRange = Physics.CheckSphere(transform.position, tackleRange, whatIsPlayer);
        if (isAttackRange && !isTackleRange) Attack();
        else if (isTackleRange) Tackle();
        else Idle();
    }

    private async void Tackle()
    {
        transform.LookAt(player.transform);
       
        bossAnimator.SetTrigger("Tackle");
       
    }

    private void Attack()
    {
        transform.LookAt(player.transform);
        bossAnimator.SetBool("isAttacking", true);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(grenade, bossHand.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
            rb.AddForce(transform.up * upForce, ForceMode.Impulse);
           
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void Idle()
    {
        bossAnimator.SetBool("isAttacking", false);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, tackleRange);
    }
}