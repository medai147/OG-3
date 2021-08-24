using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class AudioManager : ScriptableObject
{
    //[MenuItem("Assets/Create/AudioManager")]
    public void PlayOneShot(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public AudioSource audioSource{get; set;}
}
