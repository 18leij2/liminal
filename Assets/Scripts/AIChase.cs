using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AIChase : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Rigidbody rb;
    public LayerMask wallLayer;

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckTime = 1.5f;
    public float rerouteDistance = 2.0f;

    public GameObject tryAgainPanel;
    public TextMeshProUGUI tryAgainText;
    public Button tryAgainButton;

    public float catchDistance = 2.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.isKinematic = false;

        agent.autoBraking = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.avoidancePriority = 50;

        lastPosition = transform.position;

        tryAgainPanel.SetActive(false);
        tryAgainButton.onClick.AddListener(RestartGame);
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

            if (Vector3.Distance(transform.position, player.position) < catchDistance)
            {
                TriggerTryAgain();
            }
        }
    }

    bool IsPathBlocked(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target);

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
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            FindAlternativePath();
        }
    }

    void TriggerTryAgain()
    {
        tryAgainPanel.SetActive(true);
        tryAgainText.text = "It has you now...";
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
