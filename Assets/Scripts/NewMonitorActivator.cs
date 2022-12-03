using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonitorActivator : MonoBehaviour
{
    int monitor_count;

    void Start()
    {

        monitor_count = Display.displays.Length;

        for (int i = 1; i < monitor_count; i++)
        {
            Display.displays[i].Activate();
        }

        Debug.Log("monitor_count:" + monitor_count);
    }
}
