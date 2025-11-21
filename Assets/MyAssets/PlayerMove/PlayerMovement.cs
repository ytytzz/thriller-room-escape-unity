using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CharacterController controller;

    // 중력 관련
    public float gravity = -9.81f;
    private float yVelocity;

    // 애니메이터
    public Animator animator;

    void Start()
    {
        if (controller == null)
            controller = GetComponent<CharacterController>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        // ----- 중력 처리 -----
        if (controller.isGrounded)
        {
            // 땅에 닿아있을 때 Y속도를 0으로 초기화
            yVelocity = -2f; 
        }
        else
        {
            // 중력 증가
            yVelocity += gravity * Time.deltaTime;
        }

        move.y = yVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // ----- 애니메이션 처리 -----
        bool isMoving = (h != 0 || v != 0);
        animator.SetBool("is_working", isMoving);
    }
}
