using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 20;
    public Vector3 spawnArea = new Vector3(10, 10, 10);

    void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

