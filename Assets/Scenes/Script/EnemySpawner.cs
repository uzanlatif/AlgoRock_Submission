using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float delay;
    public Transform target;
    public GameObject prefab, prefab2;
    public float spawnRadius = 2.0f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy",3f,delay);
    }

    void SpawnEnemy(){
        Vector3 spawnPosition = target.transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(prefab, spawnPosition, Quaternion.identity);

        Vector3 spawnPosition2 = target.transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(prefab2, spawnPosition2, Quaternion.identity);
    }
}
