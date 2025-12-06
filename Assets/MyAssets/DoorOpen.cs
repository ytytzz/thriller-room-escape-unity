using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform doorMesh;      // 문 Mesh
    public float openAngle = -160f; // 열릴 각도
    public float openSpeed = 5f;    // 열리는 속도

    private bool isOpened = false;
    private Quaternion startRot;
    private Quaternion endRot;
    


    void Start()
    {
        if (doorMesh == null) doorMesh = transform;

        startRot = doorMesh.localRotation;
        endRot = startRot * Quaternion.AngleAxis(openAngle, Vector3.up);
    }

    public void OpenDoor()
    {
        isOpened = true;
        
        SubtitleUI.Instance.ShowLines(new string[] { "주변을 잘 둘러보고 힌트를 얻어보자!" }, 3f, 0.2f);
    }

    void Update()
    {
        if (isOpened)
        {
            doorMesh.localRotation = Quaternion.Lerp(doorMesh.localRotation, endRot, Time.deltaTime * openSpeed);
        }
    }
}
