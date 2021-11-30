using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        //create the Audio Object
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        //Set up and configure
        audioSource.clip = clip;
        audioSource.volume = volume;


        //activate it
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);

        //return just in case
        return audioSource;
    }
    
}
