using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    public float wanderRadius = 20f;

    // 움직이는 시간 / 멈춰 있는 시간 범위
    public Vector2 moveTimeRange = new Vector2(2f, 5f);
    public Vector2 pauseTimeRange = new Vector2(1f, 3f);

    NavMeshAgent agent;
    Animator animator;

    float timer;
    float currentInterval;
    bool isPaused = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 처음에는 움직이도록 세팅
        isPaused = false;
        currentInterval = Random.Range(moveTimeRange.x, moveTimeRange.y);
        SetNewDestination();
    }

    void Update()
    {
        // 속도에 따라 Idle / Walk 전환
        if (animator != null)
        {
            float speed = agent.velocity.magnitude;
            animator.SetBool("isWalking", !isPaused && speed > 0.1f);
        }

        timer += Time.deltaTime;

        // 일정 시간이 지나면 "움직이기 <-> 멈추기" 전환
        if (timer >= currentInterval)
        {
            timer = 0f;
            isPaused = !isPaused;

            if (isPaused)
            {
                // 멈추기
                agent.isStopped = true;
                currentInterval = Random.Range(pauseTimeRange.x, pauseTimeRange.y);
            }
            else
            {
                // 다시 움직이기
                agent.isStopped = false;
                currentInterval = Random.Range(moveTimeRange.x, moveTimeRange.y);
                SetNewDestination();
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * dist;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, dist, layerMask);

        return navHit.position;
    }
}
