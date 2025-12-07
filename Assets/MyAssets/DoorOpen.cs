using UnityEngine;
using System.Collections; 

public class DoorOpen : MonoBehaviour
{
    public Transform doorMesh;      // 문 Mesh
    public float openAngle = -160f; // 열릴 각도
    public float openSpeed = 5f;    // 열리는 속도

    private bool isOpened = false;
    private Quaternion startRot;
    private Quaternion endRot;
    
    public static string[] arr = {
        "주변을 잘 둘러보자..",
        "이제 키다리 아저씨를 피해서 집을 돌아다니며 키를 찾아보자..",
        "이제 집에 갈 수 있다..."
    };

    public static ArrayList lines = new ArrayList(arr); // ← 이렇게 배열로 바로 초기화



    void Start()
    {
        if (doorMesh == null) doorMesh = transform;

        startRot = doorMesh.localRotation;
        endRot = startRot * Quaternion.AngleAxis(openAngle, Vector3.up);
    }

    public void OpenDoor()
    {
        isOpened = true;
        string firstLine = (string)lines[0]; // ArrayList는 object 타입이므로 캐스팅 필요
        SubtitleUI.Instance.ShowLines(new string[] { firstLine }, 3f, 0.2f);
        lines.RemoveAt(0);
    }

    void Update()
    {
        if (isOpened)
        {
            doorMesh.localRotation = Quaternion.Lerp(doorMesh.localRotation, endRot, Time.deltaTime * openSpeed);
        }
    }
}
