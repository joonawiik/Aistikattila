using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMultiDisplays : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("ScreenActivation");

        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        foreach (var disp in Display.displays)
        {
            disp.Activate(disp.systemWidth, disp.systemHeight, 60);
        }
    }
}

