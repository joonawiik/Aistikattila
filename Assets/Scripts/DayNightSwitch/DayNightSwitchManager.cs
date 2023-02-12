using UnityEngine;
using System;
using System.Collections.Generic;

[ExecuteAlways]
public class DayNightSwitchManager : MonoBehaviour
{
    public static DayNightSwitchManager Instance {private set; get;}

    //Scene References
    [SerializeField] private DayNightSwitchPreset Preset;
    [SerializeField] private Material Skybox;
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingSettings lightingSetting;
    [SerializeField] private GameObject particleRoot;
    [SerializeField] private GameObject cloudsRoot;
    private List<Material> cloudMaterials;
    
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;
    //[SerializeField] private float AnimationDuration = 3.0f;

    [SerializeField, Range(0, 1)] private float CloudMovementSpeed = 0.08f;
    
    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // TimeOfDay += Time.deltaTime;
            // TimeOfDay %= 24;
            // UpdateLighting(TimeOfDay / 24f);
            SkyboxMovement();
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void SkyboxMovement()
    {
        TimeOfDay += Time.deltaTime;
        TimeOfDay %= 24;
        RenderSettings.skybox.SetFloat("_Rotation", Mathf.Lerp(0, 360, TimeOfDay / 24f * CloudMovementSpeed));
    }

    private void SetCloudColor(float timePercent)
    {
        foreach(var mat in cloudMaterials)
        {
            mat.SetColor("_CloudColor", Preset.cloud.color.Evaluate(timePercent));
        }
    }

    private void UpdateLighting(float timePercent)
    {
        bool isMorning = (timePercent <= 0.5f)? true: false;

        //Set environment ambient color & shadow color
        RenderSettings.ambientLight = Preset.environment.environmentLightColor.Evaluate(timePercent);
        RenderSettings.subtractiveShadowColor = Preset.environment.shadowColor.Evaluate(timePercent);
        
        //Set skybox & exposure & rotation
        RenderSettings.skybox.SetColor("_Tint", Preset.skybox.color.Evaluate(timePercent));
        RenderSettings.skybox.SetFloat("_Exposure", (isMorning)? 
            Mathf.Lerp(Preset.skybox.midNightExposure, Preset.skybox.midDayExposure, timePercent * 2.0f):
            Mathf.Lerp(Preset.skybox.midDayExposure, Preset.skybox.midNightExposure, (timePercent - 0.5f) * 2.0f));
        RenderSettings.skybox.SetFloat("_Rotation", Mathf.Lerp(0, 360, timePercent));

        // Set the directionalLight
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.directionalLight.color.Evaluate(timePercent);
            DirectionalLight.intensity = (isMorning)?
                Mathf.Lerp(Preset.directionalLight.midNightIntensity, Preset.directionalLight.midDayIntensity, timePercent * 2.0f):
                Mathf.Lerp(Preset.directionalLight.midDayIntensity, Preset.directionalLight.midNightIntensity, (timePercent - 0.5f) * 2.0f);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

        // Set fog when fog is on
        if (RenderSettings.fog == true)
        {
            RenderSettings.fogColor = Preset.fog.color.Evaluate(timePercent);
            RenderSettings.fogDensity = (isMorning)?
                Mathf.Lerp(Preset.fog.midNightDensity, Preset.fog.midDayDensity, timePercent * 2.0f):
                Mathf.Lerp(Preset.fog.midDayDensity, Preset.fog.midNightDensity, (timePercent - 0.5f) * 2.0f);
        }

        // particles
        if(particleRoot != null && (timePercent <= 0.2 || timePercent >= 0.8))
        {
            foreach(var child in particleRoot.GetComponentsInChildren<ParticleSystem>())
            {
                //..
            }
        }

        // Moving Clouds color
        SetCloudColor(timePercent);
    }


    //Called when 'Night' is selected
    public void DayToNight()
    {
        TimeOfDay = 24f;
        UpdateLighting(TimeOfDay / 24f);
        if(particleRoot != null)
        {
            particleRoot.SetActive(true);
            
            // If it rains, do not active fireflies
            if(SettingManager.Instance.weather == SettingManager.Weather.Rainy)
            {
                particleRoot.SetActive(false);
            }
        }
        SetCloudColor(TimeOfDay / 24f);
    }

    //Called when 'Day' is selected
    public void NightToDay()
    {
        TimeOfDay = 12f;
        UpdateLighting(TimeOfDay / 24f);
        if(particleRoot != null)
        {
            particleRoot.SetActive(false);
        }
        SetCloudColor(TimeOfDay / 24f);
    }


    // Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
    private void OnValidate()
    {
        // Make sure the DirectionalLight is not missing
        if (DirectionalLight == null)
        {
            //Search for lighting tab sun
            if (RenderSettings.sun != null)
            {
                DirectionalLight = RenderSettings.sun;
            }
            //Search scene for light that fits criteria (directional)
            else
            {
                Light[] lights = GameObject.FindObjectsOfType<Light>();
                foreach (Light light in lights)
                {
                    if (light.type == LightType.Directional)
                    {
                        DirectionalLight = light;
                        return;
                    }
                }
        }
        }
    
        // Shadow color only works with MixedLightingMode.Subtractive
        if(lightingSetting != null)
        {
            lightingSetting.mixedBakeMode = MixedLightingMode.Subtractive;
        }

        // Set Environment Lighting Source as 'Color' so that ambient color works
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;

        //Make Sure skybox is not missing.
        if (RenderSettings.skybox == null)
        {
            RenderSettings.skybox = Skybox;
        }

        //Make Sure the particle Root has a ParticleSystem component so as to activate all particles when focused.
        if (particleRoot != null)
        {
            var ps = particleRoot.GetComponent<ParticleSystem>();
            if(ps == null)
            {
                ps= particleRoot.AddComponent<ParticleSystem>();
            }
            
            // make sure the emission/shape is disabled so that it does not lag the compiling
            var e = ps.emission;
            e.enabled = false;
            var s = ps.shape;
            s.enabled = false;
        }
    
        //Make Sure the cloud material is not missing.
        if(cloudsRoot != null)
        {
            cloudMaterials = new List<Material>();
            foreach(var renderer in cloudsRoot.GetComponentsInChildren<Renderer>())
            {
                if(!cloudMaterials.Contains(renderer.sharedMaterial))
                {
                    cloudMaterials.Add(renderer.sharedMaterial);
                }
            }
        }
    }

    private void Start()
    {
        // TODO: check interface parameter
        TimeOfDay = 12f;
        OnValidate();
    }

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

    private void onAwake()
    {
        //Todo: read from current rendering setting onAwake?
    }
}