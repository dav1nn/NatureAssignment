using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float neighborDistance = 3.0f;
    public float separationDistance = 1.5f;
    public float obstacleAvoidanceDistance = 5.0f;
    public LayerMask obstacleLayer;

    private Vector3 direction;

    void Start()
    {
        direction = transform.forward;
    }

    void Update()
    {
        ApplyBoidRules();
        ObstacleAvoidance();
        MoveBoid();
    }

    void ApplyBoidRules()
    {
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        int neighborCount = 0;

        foreach (Boid boid in FindObjectsOfType<Boid>())
        {
            if (boid == this) continue;

            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (distance < neighborDistance)
            {
                
                if (distance < separationDistance)
                {
                    separation += (transform.position - boid.transform.position) / distance;
                }

               
                alignment += boid.direction;

        
                cohesion += boid.transform.position;

                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            separation /= neighborCount;
            alignment /= neighborCount;
            cohesion = (cohesion / neighborCount - transform.position).normalized;

            direction += separation * separationDistance + alignment + cohesion;
            direction = Vector3.Lerp(transform.forward, direction, rotationSpeed * Time.deltaTime).normalized;
        }
    }

    void ObstacleAvoidance()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceDistance, obstacleLayer))
        {
            Vector3 avoidDirection = Vector3.Reflect(transform.forward, hit.normal);
            direction = Vector3.Lerp(transform.forward, avoidDirection, rotationSpeed * Time.deltaTime).normalized;
        }
    }

    void MoveBoid()
    {
        transform.forward = Vector3.Slerp(transform.forward, direction, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
