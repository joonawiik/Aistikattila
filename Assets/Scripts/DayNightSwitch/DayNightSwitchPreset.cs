using System;
using UnityEngine;


[Serializable]
[CreateAssetMenu(fileName ="DayNightSwitch Preset", menuName ="Scriptables/DayNightSwitch Preset",order =1)]
public class DayNightSwitchPreset : ScriptableObject
{    
    public GeneralEnvironmentSetting environment;
    public SkyboxSetting skybox;
    public DirectionalLightSetting directionalLight;
    public FogSetting fog;
    public CloudSetting cloud;

    void onGUI(){}
}

[Serializable]
public class GeneralEnvironmentSetting
{
    public Gradient shadowColor;
    //Should check the source is color
    public Gradient environmentLightColor;
}

[Serializable]
public class SkyboxSetting
{
    public Gradient color;
    public float midDayExposure = 1.0f;
    public float midNightExposure = 1.0f;
    // public float midDayRotation = 1.0f;
    // public float midNightRotation = 1.0f;
}

[Serializable]
public class DirectionalLightSetting
{
    public Gradient color;
    public float midDayIntensity;
    public float midNightIntensity;
    // public float IndirectMultiplier;
}

[Serializable]
public class FogSetting
{
    public Gradient color;
    public float midDayDensity = 0.0f;
    public float midNightDensity = 0.01f;
}

[Serializable]
public class CloudSetting
{
    public Gradient color;
}