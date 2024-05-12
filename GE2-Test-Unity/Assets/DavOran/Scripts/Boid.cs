using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float neighborDistance = 3f;
    public float separationDistance = 1.5f;

    private Vector3 velocity;
    private List<Boid> allBoids;

    void Start()
    {
        velocity = transform.forward;
        allBoids = new List<Boid>(FindObjectsOfType<Boid>());
    }

    void Update()
    {
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        int neighborCount = 0;

        foreach (Boid boid in allBoids)
        {
            if (boid == this) continue;

            float distance = Vector3.Distance(boid.transform.position, transform.position);

            if (distance < neighborDistance)
            {
                if (distance < separationDistance)
                {
                    separation -= (boid.transform.position - transform.position);
                }

                alignment += boid.velocity;
                cohesion += boid.transform.position;
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            alignment /= neighborCount;
            cohesion /= neighborCount;
            cohesion = (cohesion - transform.position).normalized;
        }

        Vector3 direction = (separation + alignment + cohesion).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        velocity = transform.forward * speed;
        transform.position += velocity * Time.deltaTime;
    }
}
