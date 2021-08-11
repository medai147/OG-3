using System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startsclipt : MonoBehaviour
{
    private Text timetext;
    float countTime = 0;
    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject finishpanel;
    // Start is called before the first frame update
    void Start()
    {
        startpanel.SetActive(true);
        timetext = GameObject.Find("timetext").GetComponent<Text>();
        finishpanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startpanel.activeSelf == false)
        {
            countTime += Time.deltaTime; //スタートしてからの秒数を格納
            timetext.text = countTime.ToString("F0"); //整数にして表示
            if(countTime > 10)
            {
                finishpanel.SetActive(true);
            }
        }
    }
    public void onClicked_startbutton()
    {
        startpanel.SetActive(false);
    }

    public void onClicked_continuebutton()
    {
        //countTime = 0;
        finishpanel.SetActive(false);
        startpanel.SetActive(true);
    }

    public void onClicked_returnbutton()
    {
        //countTime = 0;
        SceneManager.LoadScene("start scene");
    }
}
