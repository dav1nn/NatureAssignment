using UnityEngine;
using UnityEngine.AI;

public class BirdController : MonoBehaviour
{
    private float walkSpeed = 3.5f;
    private float flySpeed = 5f;
    private float flyHeight = 10f;
    private float walkTime = 5f;
    private float flyTime = 3f;
    private float descentSpeed = 2f;

    private NavMeshAgent navAgent;
    private Vector3 targetPosition;
    private bool isFlying = false;
    private bool isDescending = false;
    private float timer = 0f;
    private float changeStateTime;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = walkSpeed;
        ChangeState();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeStateTime)
        {
            if (isDescending)
            {
                StartWalking();
            }
            else
            {
                ChangeState();
            }
        }

        if (isFlying)
        {
            Fly();
        }
        else if (isDescending)
        {
            Descend();
        }
        else if (navAgent.enabled)
        {
            Walk();
        }
    }

    void ChangeState()
    {
        isFlying = !isFlying;
        timer = 0f;
        changeStateTime = isFlying ? flyTime : walkTime;

        if (isFlying)
        {
            navAgent.enabled = false;
            targetPosition = GetRandomPosition() + Vector3.up * flyHeight;
        }
        else
        {
            PrepareToDescend();
        }
    }

    void Walk()
    {
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            Vector3 newPos = GetRandomPosition();
            if (navAgent.enabled && navAgent.isOnNavMesh)
            {
                navAgent.SetDestination(newPos);
            }
        }
    }

    void Fly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            timer = 0; // Reset timer for descent
            isFlying = false;
            isDescending = true;
        }
    }

    void Descend()
    {
        Vector3 targetGroundPosition = new Vector3(targetPosition.x, 0, targetPosition.z);
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, targetGroundPosition, descentSpeed * Time.deltaTime);
        nextPosition.y = Mathf.Max(0, nextPosition.y - descentSpeed * Time.deltaTime);

        transform.position = nextPosition;

        if (transform.position.y == 0)
        {
            StartWalking();
        }
    }

    void PrepareToDescend()
    {
        changeStateTime = flyTime; // Duration to descend
        isDescending = true;
    }

    void StartWalking()
    {
        navAgent.enabled = true;
        Vector3 newPos = GetRandomPosition();
        if (!navAgent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newPos, out hit, 10f, NavMesh.AllAreas))
            {
                navAgent.Warp(hit.position);
            }
        }
        navAgent.SetDestination(newPos);
        isDescending = false;
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
}
