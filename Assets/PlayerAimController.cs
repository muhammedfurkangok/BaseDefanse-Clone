using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    
    #endregion
    #region private Variables
    
    private GameObject currentEnemy; 
    
    #endregion
    #endregion
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                currentEnemy = collider.gameObject;

                Vector3 direction = currentEnemy.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}