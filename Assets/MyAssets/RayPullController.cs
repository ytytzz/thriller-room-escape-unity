using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RayPullController : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public Transform controllerTransform;

    void Update()
    {
        // 트리거 누르면 당기기
        if (Input.GetAxis("XRI_Right_Trigger") > 0.1f) 
        {
            TryPull();
        }
    }

    void TryPull()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            ForcePull pull = hit.collider.GetComponent<ForcePull>();
            if (pull != null)
            {
                pull.StartPull(controllerTransform);
            }
        }
    }
}
