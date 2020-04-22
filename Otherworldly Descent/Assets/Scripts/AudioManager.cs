using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip cave;
    public AudioClip temple;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void ChangeBGM () 
    {
        audio.Stop();
        audio.clip = temple;
        audio.Play();
    }
}
