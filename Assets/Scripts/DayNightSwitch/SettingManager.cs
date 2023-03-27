using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingDataManager
{
    public enum TimeOfDay {Day, Night};
    public enum Weather {Sunny, Rainy};
    public enum Sound {On, Off};

    public static TimeOfDay timeOfDay { get; set; }
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
            case TimeOfDay.Day:
                DayNightSwitchManager.Instance.NightToDay();
                AudioManager.Instance.SunnyAudio();
                break;
            case TimeOfDay.Night:
                DayNightSwitchManager.Instance.DayToNight();
                AudioManager.Instance.NightAudio();
                break;
            default: break;
        }

        switch(weather)
        {
            case Weather.Sunny:
                RainEffect.Instance.RainToSun();
                AudioManager.Instance.SunnyAudio();
                break;
            case Weather.Rainy:
                RainEffect.Instance.SunToRain();
                AudioManager.Instance.RainAudio();
                break;
            default: break;
        }

        switch(sound)
        {
            case Sound.On: AudioManager.Instance.AudioOn();break;
            case Sound.Off: AudioManager.Instance.AudioOff();break;
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
