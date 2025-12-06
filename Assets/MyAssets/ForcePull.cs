using UnityEngine;

public class ForcePull : MonoBehaviour
{
    public Transform pullTarget;    // 잡아당길 목적지(컨트롤러)
    public float pullSpeed = 5f;    // 기본 속도
    public float maxSpeed = 3f;     // 절대 최대 속도(폭주 방지)
    public float stopDistance = 0.2f;

    private Rigidbody rb;
    private bool isPulling = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isPulling || pullTarget == null) return;

        Vector3 direction = pullTarget.position - transform.position;
        float distance = direction.magnitude;

        // 너무 가까우면 종료
        if (distance < stopDistance)
        {
            rb.velocity = Vector3.zero;
            isPulling = false;
            rb.useGravity = true;
            return;
        }

        // 거리 비례 속도 + 속도 제한
        float speed = Mathf.Min(pullSpeed * distance, maxSpeed);

        Vector3 move = direction.normalized * speed;

        // 물리력 대신 MovePosition 사용 → 폭주 없음 + 벽 안 뚫음
        rb.MovePosition(transform.position + move * Time.fixedDeltaTime);
    }

    public void StartPull(Transform target)
    {
        pullTarget = target;
        isPulling = true;
        rb.useGravity = false;
    }

    public void StopPull()
    {
        isPulling = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
    }
}
