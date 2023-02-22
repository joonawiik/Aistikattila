using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceManager : MonoBehaviour
{
    private float backgroundRotation;
    [SerializeField, Range(0, 1)] private float BackgroundMovementSpeed = 0.08f;

    public GameObject starsRoot;
    private ParticleSystem[] particles;
    private List<float> particleSpeeds;
    private List<float> particleRotations;

    [SerializeField, Range(0, 1)] private float particleSpeedModifier = 0.08f;


    // Start is called before the first frame update
    void Start()
    {
        backgroundRotation = 0.0f;

        // Find star particles;
        if(starsRoot != null)
        {
            particles = starsRoot.GetComponentsInChildren<ParticleSystem>();
        }
        // Set proper speed for each depth of field
        particleSpeeds = new List<float>();
        particleRotations = new List<float>();
        for(int i = 0; i < particles.Length; i ++)
        {
            particleSpeeds.Add(BackgroundMovementSpeed * (particles.Length - i) / particles.Length * particleSpeedModifier);
            particleRotations.Add(0f);
            Debug.Log(particleSpeeds[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        BackgroundMovement();
        StarsMovement();
    }

    private void BackgroundMovement()
    {
        backgroundRotation += Time.deltaTime * BackgroundMovementSpeed;
        backgroundRotation %= 360;
        RenderSettings.skybox.SetFloat("_Rotation", Mathf.Lerp(0, 360, backgroundRotation / 360f));
    }

    private void StarsMovement()
    {
        for(int i = 0; i < particles.Length; i++)
        {
            particleRotations[i] += Time.deltaTime * particleSpeeds[i];
            particleRotations[i] %= 360;
            particles[i].transform.Rotate(
                0, - particleRotations[i]/360f, 0, Space.Self
            );
        }
    }
}
