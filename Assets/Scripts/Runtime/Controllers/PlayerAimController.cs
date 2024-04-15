using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables
    [SerializeField] private float sightRange = 5f;
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameObject childRotation;
    
    #endregion
    #region private Variables
    private LayerMask WhatIsEnemy;

    public GameObject currentEnemy;

    
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
                Vector3 direction = currentEnemy.transform.position - childRotation.transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                childRotation.transform.rotation = Quaternion.Lerp(childRotation.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(childRotation.transform.position, sightRange);
    }
}