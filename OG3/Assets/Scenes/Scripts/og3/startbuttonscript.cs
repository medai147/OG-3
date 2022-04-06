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
    [SerializeField] GameObject startbutton;
    static int count;
    public static bool start = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if(InputNamePanel.activeSelf == true) {
            startbutton.SetActive(false);
        } else
        {
            startbutton.SetActive(true);
        }
    }
    public void onClicked_startButton()
    {
        Invoke("jumptoStart", 0.52f);
        //InputNamePanel.SetActive(true);
        PlayerPrefs.SetInt("NUMBERLOAD", 0);
        PlayerPrefs.Save();
        start = true;
    }

    public void jumptoStart()
    {
        InputNamePanel.SetActive(true);
    }
}
