using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region Self Variables

    #region Private Variables

    private LayerMask WhatIsPlayer;
    
    private bool isAttackRange;

    #endregion

    #region Serialized Variables
    
    [SerializeField] private float AttackRange;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private GameObject Granade;

    #endregion
    #endregion
    void Update()
    {
        isAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
        if (isAttackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        transform.LookAt(player.transform);
        bossAnimator.SetBool("isAttacking", true);
        ThrowGranade();
        
    }

    private void ThrowGranade()
    {
        Instantiate(Granade, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
