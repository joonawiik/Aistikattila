using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignMonitor : MonoBehaviour
{
    private void Awake()
    {
        if (GetComponent<CameraInput>().cameraIndex == 1)
        {
            Camera cam = GetComponentInChildren<Camera>();
            cam.targetDisplay = 1;
        }
    }
}
