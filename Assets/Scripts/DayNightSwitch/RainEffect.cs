using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    private GameObject rain;

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

        // Instantiate rain effects
        if(rain == null)
        {
            var gameObj = GameObject.Find("Raindrops");
            if(gameObj != null)
            {
                rain = gameObj;
            }
            else
            {
                rain = Instantiate (Resources.Load ("Prefabs/Raindrops")) as GameObject;
                rain.name = "Raindrops";
            }
        }
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
