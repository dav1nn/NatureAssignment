using UnityEngine;
using UnityEngine.AI;

public class SnailController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float roamRadius = 20f;
    public float roamDelay = 2f;

    private float roamTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        roamTimer = roamDelay;
    }

    void Update()
    {
        roamTimer += Time.deltaTime;

        if (roamTimer >= roamDelay)
        {
            Vector3 newDestination = RandomNavMeshLocation(roamRadius);
            agent.SetDestination(newDestination);
            roamTimer = 0;
        }
    }

    public Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
