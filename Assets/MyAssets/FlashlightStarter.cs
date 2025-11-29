using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightStarter : MonoBehaviour
{
    public Transform leftHand;      // LeftHand Controller
    public Transform attachPoint;    // LeftHandAttach

    void Start()
    {
        transform.SetParent(leftHand);
        transform.localPosition = attachPoint.localPosition;
        transform.localRotation = attachPoint.localRotation;
    }
}

