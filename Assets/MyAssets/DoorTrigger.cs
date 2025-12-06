using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorOpen door;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {

            Debug.Log("OpenDoor 호출됨");
            door.OpenDoor();
            Destroy(other.gameObject);
        }
    }
}
