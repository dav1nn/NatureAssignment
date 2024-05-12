using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject boidPrefab;
    public int boidCount = 20;
    public float spawnRadius = 10f;
    public Transform groundTransform;

    private void Start()
    {
        for (int i = 0; i < boidCount; i++)
        {
            Vector3 spawnPosition = groundTransform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = groundTransform.position.y + 1f; 
            Instantiate(boidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
