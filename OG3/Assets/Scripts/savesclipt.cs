using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class savesclipt : MonoBehaviour
{
    private int save;
    private static int load;
    private Text _save1;
    private Text _save2;
    private Text _save3;
    private Text _save4;
    private static int number;
    private static int number1;
    private static int number2;
    private static int number3;
    private static int number4;
    // Start is called before the first frame update
    void Start()
    {
        save = PlayerPrefs.GetInt("SAVE");
        PlayerPrefs.SetInt("SAVE", 0);
        number = PlayerPrefs.GetInt("NUMBER");
        //ここに飛びたいストーリー番号を書いてsave画面から始める　例  number = 34;

        _save1 = GameObject.Find("save1text").GetComponent<Text>();
        _save2 = GameObject.Find("save2text").GetComponent<Text>();
        _save3 = GameObject.Find("save3text").GetComponent<Text>();
        _save4 = GameObject.Find("save4text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _save1.text = number1.ToString("000");
        _save2.text = number2.ToString("000");
        _save3.text = number3.ToString("000");
        _save4.text = number4.ToString("000");
        Debug.Log(load);
    }
    public void save1_onClicked()
    {
        Debug.Log(save);
        if (load != 1)
        {
            number1 = number;
            _save1.text = "記録されました";
            SceneManager.LoadScene("start scene");
        }
        else if (load == 1)
        {
            if (number1 == 0)
            {
                PlayerPrefs.SetInt("NUMBERLOAD", number1);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("NUMBERLOAD", number1 - 1);
                PlayerPrefs.Save();
            }

            load = 0;
            SceneManager.LoadScene("main scene");
        }
    }

    public void save2_onClicked()
    {
        Debug.Log(save);
        Save(ref number2, ref _save2);
    }

    public void save3_onClicked()
    {
        Debug.Log(save);
        Save(ref number3, ref _save3);
    }

    public void save4_onClicked()
    {
        Debug.Log(save);
        Save(ref number4, ref _save4);
    }



    public void Save(ref int savenum, ref Text savetext)
    {
        if (load != 1)

        {
            savenum = number;
            savetext.text = "記録されました";
            SceneManager.LoadScene("start scene");
        }
        else if (load == 1)
        {
            if (savenum == 0)
            {
                PlayerPrefs.SetInt("NUMBERLOAD", savenum);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("NUMBERLOAD", savenum - 1);
                PlayerPrefs.Save();
            }

            load = 0;
            SceneManager.LoadScene("main scene");
        }
    }
    public void load_onClicked()
    {
        save = 0;
        load = 1;
        SceneManager.LoadScene("save scene");
    }

}
