using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
    [SerializeField] private GameObject grenadeExplosionArea; 
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float forwardForce;
    [SerializeField] private float upForce;
    [SerializeField] private ParticleSystem ExplosionEffect;

    #endregion
    
    private bool isAttackRange;
    private bool isTackleRange;
    private bool alreadyAttacked;
    private bool tackled;
    

    void Update()
    {
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        isTackleRange = Physics.CheckSphere(transform.position, tackleRange, whatIsPlayer);
        if (isAttackRange && !isTackleRange) Attack();
        else if (isTackleRange && !tackled) Tackle();
        else if (!isAttackRange && !isTackleRange) Idle() ;
    }

    private  void Tackle()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();
         tackled = true;
        transform.LookAt(player.transform);
        bossAnimator.SetTrigger("Tackle");
        playerRb.AddForce(transform.forward * 50, ForceMode.Impulse);
       
    }

    private  void Attack()
    {
        transform.LookAt(player.transform);
        tackled = false;
        
        if (!alreadyAttacked)
        {
              bossAnimator.SetTrigger("Throw");
              StartCoroutine(ThrowGranade());
         
              alreadyAttacked = true;
              Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ExplosionArea()
    {
        var obj =Instantiate(grenadeExplosionArea, player.transform.position, grenadeExplosionArea.transform.rotation);
        obj.transform.DOScale(new Vector3(4, 4, 4), 0.5f);
        obj.transform.DOScale(Vector3.zero, 0.5f)
            .SetDelay(1f)
            .OnComplete(() => Destroy(obj));
    }

    private IEnumerator  ThrowGranade()
    {
        yield return new WaitForSeconds(1f);
        Vector3 startPos = bossHand.transform.position;
        Vector3 midPos = (startPos + player.transform.position) / 2f + Vector3.up * 4; // Yükseklik ekle
        Vector3 endPos = player.transform.position;
        
        Vector3[] path = new Vector3[] { startPos, midPos, endPos };
        ExplosionArea();
        
        GameObject obj = Instantiate(grenade, startPos, grenade.transform.rotation);
        obj.transform.DORotate(new Vector3(Random.Range(0,360),360, Random.Range(0,360)), 2f);
        obj.transform.DOPath(path, 1, PathType.CatmullRom)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                ExplosionEffect.transform.position = obj.transform.position;
                ExplosionEffect.Play();
                Destroy(obj);
               
              
            });
    }

   
    private void Idle()
    {
      transform.LookAt(player.transform);
      tackled = false;
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