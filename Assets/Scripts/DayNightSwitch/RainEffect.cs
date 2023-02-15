using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    [SerializeField] private GameObject rain;

    public static RainEffect Instance;

    private void Awake()
    {
        //singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void RainToSun()
    {
        rain.SetActive(false);
    }

    public void SunToRain()
    {
        rain.SetActive(true);
    }
}
