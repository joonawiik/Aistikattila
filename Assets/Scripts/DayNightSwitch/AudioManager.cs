using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance {private set; get;}
    
    [SerializeField] private GameObject audio;
    [SerializeField] private AudioClip sunny;
    [SerializeField] private AudioClip rainy;
    [SerializeField] private AudioClip night;

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

    public void AudioOn()
    {
        audio.SetActive(true);
    }

    public void AudioOff()
    {
        audio.SetActive(false);
    }

    public void SunnyAudio() {
        AudioSource source = audio.GetComponent<AudioSource>();
        source.clip = sunny;
        source.Play();
    }

    public void RainAudio() {
        AudioSource source = audio.GetComponent<AudioSource>();
        source.clip = rainy;
        source.Play();
    }

    public void NightAudio() {
        AudioSource source = audio.GetComponent<AudioSource>();
        source.clip = night;
        source.Play();
    }
}
