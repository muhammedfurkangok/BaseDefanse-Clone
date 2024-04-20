using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaManager: MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private GameObject payPlace;
    [SerializeField] private GameObject ActivePlace;
    [SerializeField] private GameObject DeactivePlace;
    [SerializeField] private GameObject[] paths;
    [SerializeField] private TextMeshPro gemText;
    [SerializeField] private Ease ease;
    [SerializeField] private int price;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            if (gemText.text != "0")
            {
                if (uiManager.gemCount > 1)
                {
                
                    Vector3 randomPoint = paths[Random.Range(0, paths.Length)].transform.position;
            
       
                    int currentMoney = int.Parse(gemText.text);
                    int remainingMoney = currentMoney - 2;
                    gemText.text = remainingMoney.ToString();
                    uiManager.PayGem(2);

            
                    Vector3 instantiationPosition = player.transform.position; 
                    var obj = Instantiate(gemPrefab, instantiationPosition, Quaternion.Euler(180, 0, 0));
                    obj.transform.DOPath(new Vector3[] { randomPoint, payPlace.transform.position }, 1f, PathType.CatmullRom)
                        .SetOptions(false)
                        .SetEase(ease)
                        .OnComplete(() =>
                        {
                            Destroy(obj);
                        });
                }
            }
            else if (gemText.text == "0")
            {
               DeactivePlace.SetActive(false);
               ActivePlace.SetActive(true);
            }
        }
        
       
    }
}