using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;   // 최초 스폰 위치 저장
    }

    public void Respawn()
    {
        transform.position = initialPosition;   // 위치 리셋
    }
}
