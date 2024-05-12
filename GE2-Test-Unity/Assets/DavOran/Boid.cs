using UnityEngine;

public class Boid : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 3f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public float maxFlyHeight = 10f;
    public float flightDuration = 5f;

    private Vector3 velocity;
    private bool isFlying;
    private float flightTimer;
    private Vector3 groundPosition;

    private void Start()
    {
        velocity = Random.insideUnitSphere;
        velocity.y = 0;
        velocity = velocity.normalized * speed;
        groundPosition = transform.position;
        StartFlying();
    }

    private void Update()
    {
        if (isFlying)
        {
            Fly();
        }
        else
        {
            Walk();
        }

        if (isFlying && flightTimer <= 0)
        {
            Land();
        }
    }

    private void Fly()
    {
        flightTimer -= Time.deltaTime;

        velocity += Random.insideUnitSphere * 0.1f;
        velocity = velocity.normalized * speed;


        if (transform.position.y > maxFlyHeight)
        {
            velocity.y = -Mathf.Abs(velocity.y);
        }


        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);
    }

    private void Walk()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer))
        {
            velocity.y -= 9.81f * Time.deltaTime; 
        }
        else
        {
            velocity.y = 0; 
        }


        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);


        if (Random.value < 0.01f)
        {
            StartFlying();
        }
    }

    private void StartFlying()
    {
        isFlying = true;
        flightTimer = flightDuration;
        velocity = Random.insideUnitSphere * speed;
        groundPosition = transform.position;
    }

    private void Land()
    {
        isFlying = false;
        velocity.y = 0;
        transform.position = new Vector3(transform.position.x, groundPosition.y, transform.position.z);
    }
}
