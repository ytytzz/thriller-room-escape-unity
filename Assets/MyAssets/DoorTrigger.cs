using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorOpen door; // 실제 문 오브젝트 참조

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {
            Debug.Log("OpenDoor 호출됨");
            door.OpenDoor();
        }
    }


}
