using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    // 👉 체크포인트(웨이포인트)들
    public Transform[] waypoints;
    public float waypointTolerance = 0.5f;   // 목적지 도착 판정 오차

    NavMeshAgent agent;
    Animator animator;

    int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("WanderAI: Waypoints가 비어 있습니다.", this);
            enabled = false;
            return;
        }

        agent.isStopped = false;
        SetNewDestination();
    }

    void Update()
    {
        // 애니메이션 (Idle / Walk 전환)
        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetBool("isWalking", speed > 0.1f);
        }

        // 목적지에 거의 도착하면 다음 웨이포인트로
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance + waypointTolerance)
        {
            GoToNextWaypoint();
        }
    }

    void SetNewDestination()
    {
        Vector3 targetPos = waypoints[currentWaypointIndex].position;
        agent.SetDestination(targetPos);
    }

    void GoToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetNewDestination();
    }

    // 🔥 추가된 부분: 플레이어 충돌 시 스폰 위치로 되돌리기
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Respawn();
            }
        }
    }
}
