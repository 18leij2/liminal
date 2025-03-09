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
    private Animator animator; // Reference to Animator
    public LayerMask wallLayer;

    private Vector3 lastPosition;
    private float stuckTimer = 0f;
    public float stuckCheckTime = 1.5f;
    public float rerouteDistance = 2.0f;

    public GameObject tryAgainPanel;
    public TextMeshProUGUI tryAgainText;
    public Button tryAgainButton;

    // FOR PLAYING SOUNDS
    public AudioSource audioSource;
    public AudioClip loseSound;

    public float catchDistance = 2.0f;

    void Start()
    {
        // needed to add this to fix restart not unpausing
        Time.timeScale = 1f;

        // adding audio source
        audioSource = GetComponent<AudioSource>();

        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get the Animator component

        rb.freezeRotation = true;
        rb.isKinematic = false;

        agent.autoBraking = true;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        agent.avoidancePriority = 50;

        lastPosition = transform.position;

        tryAgainPanel.SetActive(false);
        tryAgainButton.onClick.AddListener(RestartGame);
        
        SetAnimationState("isIdle"); // Start with Idle
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            Vector3 targetPosition = player.position;

            if (distanceToPlayer > catchDistance) // If far away, chase the player
            {
                if (!IsPathBlocked(targetPosition))
                {
                    agent.SetDestination(targetPosition);
                    SetAnimationState("isChasing");
                }
                else
                {
                    FindAlternativePath();
                    SetAnimationState("isWalking");
                }
            }
            else // Close enough to attack
            {
                SetAnimationState("isAttacking");
                TriggerTryAgain();
            }

            if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
            {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > stuckCheckTime)
                {
                    FindAlternativePath();
                    stuckTimer = 0f;
                    SetAnimationState("isWalking");
                }
            }
            else
            {
                stuckTimer = 0f;
            }

            lastPosition = transform.position;
        }
    }

    void SetAnimationState(string newState)
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false);

        animator.SetBool(newState, true);
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
            SetAnimationState("isWalking");
        }
    }

    void TriggerTryAgain()
    {
        tryAgainPanel.SetActive(true);
        tryAgainText.text = "It has you now...";
        Time.timeScale = 0f;

        // play the sound
        audioSource.PlayOneShot(loseSound);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
