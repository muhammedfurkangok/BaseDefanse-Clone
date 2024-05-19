using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HostageSpawnerManager : MonoBehaviour
{
    public GameObject hostagePrefab;
    public int numberOfHostages;
    public float xSpawnRadius;
    public float zSpawnRadius;
    public float spawnHeight;
    public float spawnInterval;

    private void Start()
    {
        StartCoroutine(SpawnHostages());
    }

    private IEnumerator SpawnHostages()
    {
        for (int i = 0; i < numberOfHostages; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-xSpawnRadius, xSpawnRadius), spawnHeight, transform.position.z +  Random.Range(-zSpawnRadius, zSpawnRadius));
            Instantiate(hostagePrefab, spawnPosition, hostagePrefab.transform.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, xSpawnRadius);
        Gizmos.DrawWireSphere(transform.position, zSpawnRadius);
    }
}
