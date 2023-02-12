using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public enum TimeOfDay {Day, Night};
    public enum Weather {Sunny, Rainy};
    public enum Sound {ON, Off};
    public static SettingManager Instance {private set; get;}

    public TimeOfDay timeOfDay;
    public Weather weather;
    public Sound sound;

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

    public void AdjustScene(TimeOfDay t, Weather w, Sound s)
    {
        timeOfDay = t;
        weather = w;
        sound = s;

        switch (t)
        {
            case TimeOfDay.Day: DayNightSwitchManager.Instance.NightToDay(); break;
            case TimeOfDay.Night: DayNightSwitchManager.Instance.DayToNight(); break;
            default: break;
        }

        switch(w)
        {
            case Weather.Sunny: RainEffect.Instance.RainToSun();break;
            case Weather.Rainy: RainEffect.Instance.SunToRain();break;
            default: break;
        }

        switch(s)
        {
            // TODO: ..
            default: break;
        }
    }

    private void Start()
    {
        SettingManager.Instance.AdjustScene(
            SettingManager.TimeOfDay.Night, 
            SettingManager.Weather.Rainy,
            SettingManager.Sound.ON);
    }
}
