using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public LayerMask wallLayer; // Assign the wall layer in Inspector

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckTime = 1.5f; // Time before considering AI stuck
    public float rerouteDistance = 2.0f; // Distance to offset when rerouting

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        // Rigidbody settings to prevent phasing through walls
        rb.freezeRotation = true;
        rb.isKinematic = false; // Allow physics interactions

        // Adjust NavMeshAgent settings
        agent.autoBraking = true; // Stops AI from sliding
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.avoidancePriority = 50;

        lastPosition = transform.position;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position;

            if (!IsPathBlocked(targetPosition))
            {
                agent.SetDestination(targetPosition);
            }
            else
            {
                FindAlternativePath();
            }

            // Check if the AI is stuck
            if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > stuckCheckTime)
                {
                    FindAlternativePath();
                    stuckTimer = 0f;
                }
            }
            else
            {
                stuckTimer = 0f;
            }

            lastPosition = transform.position;
        }
    }

    bool IsPathBlocked(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target);

        // Check for walls using SphereCast instead of Raycast for better detection
        return Physics.SphereCast(transform.position, 0.5f, direction, out _, distance, wallLayer);
    }

    void FindAlternativePath()
    {
        Vector3 randomDirection = Random.insideUnitSphere * rerouteDistance;
        randomDirection.y = 0;

        Vector3 newTarget = transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(newTarget, out hit, rerouteDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the AI collides with a wall, back off and reroute
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            FindAlternativePath();
        }
    }
}
