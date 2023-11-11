using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Save_newScript : MonoBehaviour
{
    [SerializeField] GameObject[] savebuttons = new GameObject[4];

    String[] heroinname = new string[4];
    int[] storynum = new int[4];
    String[] photo = new string[4];
    // Start is called before the first frame update
    void Start()
    {
        AssetDatabase.Refresh();
        Debug.Log("Start");
        for(int i = 0; i < heroinname.Length;i++)
        {
            heroinname[i] = PlayerPrefs.GetString("heroin" + i);
            storynum[i] = PlayerPrefs.GetInt("storynum" + i);
            photo[i] = PlayerPrefs.GetString("photo" + i);
            
            savebuttons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "写真：" + photo[i] + "主人公の名前：" + heroinname[i] + "ストーリー番号：" + storynum[i];
            savebuttons[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(photo[i]);
            //savebuttons[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/UI/logo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("一つ前のシーン：" + GameManager.instance.beforescene);
    }

    public void onClicked_closebutton()
    {
        GameManager.instance.beforescene = 2;
        SceneManager.LoadScene("startscene_new");
    }

    
    public void onClicked_returnbutton()
    {
        GameManager.instance.beforescene = 2;
    }

    int buttonnumber = 0;
    public void onClicked_savebutton(int number)
    {
        if (GameManager.instance.beforescene == 1)
        {
            //セーブ処理
            PlayerPrefs.SetString("photo" + number, GameManager.instance.screenshotpath);
            PlayerPrefs.Save();
            PlayerPrefs.SetString("heroin" + number, GameManager.instance.heroinename);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("storynum" + number, GameManager.instance.storynum);
            PlayerPrefs.Save();
            buttonnumber = number;
            savebuttons[number].transform.GetChild(0).gameObject.GetComponent<Text>().text = "写真：" + GameManager.instance.screenshotpath + "主人公の名前：" + GameManager.instance.heroinename + "ストーリー番号：" + GameManager.instance.storynum;
            savebuttons[number].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(GameManager.instance.screenshotpath);
            Time.timeScale = 1;
            //Debug.Log("セーブ：主人公の名前：" + GameManager.instance.heroinename + "ストーリー番号：" + GameManager.instance.storynum + "一つ前のシーン：" + GameManager.instance.beforescene);
        } else
        {
            GameManager.instance.heroinename = heroinname[number];
            GameManager.instance.storynum = storynum[number];
            GameManager.instance.screenshotpath = photo[number];
            if(GameManager.instance.storynum != 0)
            {
                SceneManager.LoadScene("Mainscene_new");
            }
            Time.timeScale = 1;
            Debug.Log("ロード：主人公の名前：" + GameManager.instance.heroinename + "ストーリー番号：" + GameManager.instance.storynum + "一つ前のシーン：" + GameManager.instance.beforescene);
        }
    }


}
