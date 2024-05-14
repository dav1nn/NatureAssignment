using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float neighbourDistance = 3.0f;
    public float separationDistance = 1.5f;
    public float obstacleAvoidanceDistance = 5.0f;
    public LayerMask obstacleLayer;

    private Vector3 _direction;
    public Vector3 direction 
    { 
        get { return _direction; } 
        set { _direction = value.normalized; }
    }

    private float changeDirectionInterval = 5.0f; 
    private float lastDirectionChangeTime = 0;

    void Start()
    {
        _direction = transform.forward;
    }

    void Update()
    {
        ApplyBoidRules();
        ObstacleAvoidance();
        MoveBoid();

       
        if (Time.time - lastDirectionChangeTime > changeDirectionInterval)
        {
            ChangeRandomDirection();
            lastDirectionChangeTime = Time.time;
        }
    }

    void ApplyBoidRules()
    {
        Vector3 separationVector = Vector3.zero;
        Vector3 alignmentVector = Vector3.zero;
        Vector3 cohesionVector = Vector3.zero;
        int neighbourCount = 0;

        foreach (Boid boid in FindObjectsOfType<Boid>())
        {
            if (boid != this)
            {
                float distance = Vector3.Distance(transform.position, boid.transform.position);
                if (distance < neighbourDistance)
                {
                    if (distance < separationDistance)
                    {
                        separationVector += (transform.position - boid.transform.position) / (distance * distance);
                    }
                    alignmentVector += boid.direction;
                    cohesionVector += boid.transform.position;
                    neighbourCount++;
                }
            }
        }

        if (neighbourCount > 0)
        {
            separationVector /= neighbourCount;
            alignmentVector /= neighbourCount;
            cohesionVector = (cohesionVector / neighbourCount - transform.position).normalized;
            
            direction += (separationVector * 1.5f + alignmentVector * 1.0f + cohesionVector * 1.0f);
            direction = Vector3.Lerp(transform.forward, direction.normalized, rotationSpeed * Time.deltaTime);
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

    void ChangeRandomDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        direction += randomDirection.normalized * 0.1f; 
    }
}
