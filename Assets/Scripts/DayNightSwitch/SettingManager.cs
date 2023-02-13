using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingDataManager
{
    public enum TimeOfDay {Day, Night};
    public enum Weather {Sunny, Rainy};
    public enum Sound {ON, Off};

    private static TimeOfDay timeOfDay { get; set; }
    private static Weather weather { get; set; }
    private static Sound sound { get; set; }

    public static bool IsRainy()
    {
        return (weather == Weather.Rainy);
    }

    public static void PassData(TimeOfDay t, Weather w, Sound s)
    {
        timeOfDay = t;
        weather = w;
        sound = s;
    }

    public static void AdjustScene()
    {
        switch (timeOfDay)
        {
            case TimeOfDay.Day: DayNightSwitchManager.Instance.NightToDay(); break;
            case TimeOfDay.Night: DayNightSwitchManager.Instance.DayToNight(); break;
            default: break;
        }

        switch(weather)
        {
            case Weather.Sunny: RainEffect.Instance.RainToSun();break;
            case Weather.Rainy: RainEffect.Instance.SunToRain();break;
            default: break;
        }

        switch(sound)
        {
            // TODO: ..
            default: break;
        }
    }

}


public class SettingManager : MonoBehaviour
{
    private void Start()
    {
        SettingDataManager.AdjustScene();
    }
}
