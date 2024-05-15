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
    public float circleRadius = 50.0f;      public Vector3 circleCenter = Vector3.zero;  
    private Vector3 _direction;
    private float angle;  

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
        angle = Random.Range(0.0f, 360.0f); 
    }

    void Update()
    {
        ApplyBoidRules();
        ObstacleAvoidance();
        FlyInCircle();  
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

    void FlyInCircle()
    {
        angle += speed * Time.deltaTime;  
        if (angle >= 360.0f) angle -= 360.0f; 

        
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * circleRadius + circleCenter.x;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * circleRadius + circleCenter.z;
        Vector3 newPos = new Vector3(x, transform.position.y, z);  
        
        
        direction = (newPos - transform.position).normalized;
    }

    void ChangeRandomDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        direction += randomDirection.normalized * 0.1f;
    }
}