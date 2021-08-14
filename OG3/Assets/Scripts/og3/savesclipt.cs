using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class savesclipt : MonoBehaviour
{
    public GameObject loadstill;
    public Sprite loadsprite;
    private String heroinname;
    private static String[]heroin = { "", "", "", "" };
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
    private static DateTime dt1;
    private static DateTime dt2;
    private static DateTime dt3;
    private static DateTime dt4;
    private static int[]count = { 0, 0, 0, 0 };
    [SerializeField] GameObject loadPanel;


    // Start is called before the first frame update
    void Start()
    {
        save = PlayerPrefs.GetInt("SAVE");
        PlayerPrefs.SetInt("SAVE", 0);
        number = PlayerPrefs.GetInt("NUMBER");
        heroinname = PlayerPrefs.GetString("INPUTNAME");
        //ここに飛びたいストーリー番号を書いてsave画面から始める　例  number = 34;

        _save1 = GameObject.Find("save1text").GetComponent<Text>();
        _save2 = GameObject.Find("save2text").GetComponent<Text>();
        _save3 = GameObject.Find("save3text").GetComponent<Text>();
        _save4 = GameObject.Find("save4text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _save1.text = number1.ToString("000") + "日付" + dt1;
        _save2.text = number2.ToString("000") + "日付" + dt2;
        _save3.text = number3.ToString("000") + "日付" + dt3;
        _save4.text = number4.ToString("000") + "日付" + dt4;
        if (count[0] == 0)
        {
            _save1.text = "セーブできるよ";
        }
        if(count[1] == 0)
        {
            _save2.text = "セーブできるよ";
        }
        if (count[2] == 0)
        {
            _save3.text = "セーブできるよ";
        }
        if (count[3] == 0)
        {
            _save4.text = "セーブできるよ";
        }
        //Debug.Log(load);
    }
    public void save1_onClicked()
    {
        Save(ref number1, ref _save1, ref dt1,ref count[0],ref heroin[0]);
    }

    public void save2_onClicked()
    {
        Debug.Log(save);
        Save(ref number2, ref _save2,ref dt2,ref count[1], ref heroin[1]);
    }

    public void save3_onClicked()
    {
        Debug.Log(save);
        Save(ref number3, ref _save3,ref dt3, ref count[2],ref heroin[2]);
    }

    public void save4_onClicked()
    {
        Save(ref number4, ref _save4,ref dt4, ref count[3], ref heroin[3]);
    }



    public void Save(ref int savenum, ref Text savetext,ref DateTime dt,ref int savecount,ref string name)
    {
        if (load != 1)

        {
            savenum = number;
            dt = DateTime.Now;
            savecount = 1;
            name = heroinname;
            Debug.Log(name);
            savetext.text = "記録されました";
            SceneManager.LoadScene("start scene");
        }
        else if (load == 1)
        {
            if (savenum == 0)
            {
                loadPanel.SetActive(true);
                Image loadimage = (Image)loadstill.GetComponent<Image>();
                loadimage.sprite = loadsprite;
                PlayerPrefs.SetInt("NUMBERLOAD", savenum);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("NAMEINPUT", 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("INPUTNAME2", name);
                PlayerPrefs.Save();
            }
            else
            {
                loadPanel.SetActive(true);
                Image loadimage = (Image)loadstill.GetComponent<Image>();
                loadimage.sprite = loadsprite;
                PlayerPrefs.SetInt("NUMBERLOAD", savenum - 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("NAMEINPUT", 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("INPUTNAME2", name);
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
