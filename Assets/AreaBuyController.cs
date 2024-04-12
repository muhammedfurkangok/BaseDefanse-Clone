using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaBuyController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameObject payPlace;
    [SerializeField] private GameObject[] paths;
    [SerializeField] private TextMeshPro moneyText;
    [SerializeField] private Ease ease;
    [SerializeField] private int price;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && moneyText.text != "0")
        {
            if (uiManager.moneyCount > 0)
            {
                
            Vector3 randomPoint = paths[Random.Range(0, paths.Length)].transform.position;
            
       
            int currentMoney = int.Parse(moneyText.text);
            int remainingMoney = currentMoney - 1;
            moneyText.text = remainingMoney.ToString();
            uiManager.PayMoney(1);

            
            Vector3 instantiationPosition = player.transform.position; 
            var obj = Instantiate(moneyPrefab, instantiationPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
            obj.transform.DOPath(new Vector3[] { randomPoint, payPlace.transform.position }, 2f, PathType.CatmullRom)
                .SetOptions(false)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    Destroy(obj);
                });
            }
        }
    }
}