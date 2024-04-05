using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private float followSpeed;

    public void UpdateMoneyPosition(Transform followedMoney, bool isFollowStart)
    {
        StartCoroutine(StartFollowingToLastMoneyPosition(followedMoney, isFollowStart));
    }

    IEnumerator StartFollowingToLastMoneyPosition(Transform followedMoney, bool isFollowStart)
    {

        while (isFollowStart)
        {
            yield return new WaitForEndOfFrame();
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followedMoney.position.x, followSpeed * Time.deltaTime),
                transform.position.y,
                Mathf.Lerp(transform.position.z, followedMoney.position.z, followSpeed * Time.deltaTime));
        }
    }
}
