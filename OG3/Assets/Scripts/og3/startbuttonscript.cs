using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startbuttonscript: MonoBehaviour
{
    [SerializeField] GameObject InputNamePanel;
    static int count;

    public AudioClip startbgm;
    public AudioClip buttonclicked_se;
    AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        sounds[0].clip = startbgm;
        sounds[0].Play();

        InputNamePanel.SetActive(false);
        if (count == 0)
        {
            PlayerPrefs.SetInt("COUNTERCOUNT", count);
            PlayerPrefs.Save();
        }
        count++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClicked_startButton()
    {
        sounds[1].PlayOneShot(buttonclicked_se);
        InputNamePanel.SetActive(true);
        PlayerPrefs.SetInt("NUMBERLOAD", 0);
        PlayerPrefs.Save();
    }
}
