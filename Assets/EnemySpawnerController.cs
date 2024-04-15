using System.Collections;
using Runtime.Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private int maxEnemyCount = 80;
    [SerializeField] private float minSpawnRate = 3f;
    [SerializeField] private float maxSpawnRate = 8f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));

            if (enemyController.enemyCount < maxEnemyCount)
            {
                InstantiateEnemy();
                enemyController.enemyCount++;
                print(enemyController.enemyCount);
            }
        }
    }

    private void InstantiateEnemy()
    {
        Instantiate(enemyPrefab, transform.position, enemyPrefab.transform.rotation);
    }
}