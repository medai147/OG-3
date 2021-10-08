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
    private static String[]heroin = { "", "", "", "" , "", "", "", "" };
    private int save;
    private static int load;
    public Text _save1;
    public Text _save2;
    public Text _save3;
    public Text _save4;
    public Text _save5;
    public Text _save6;
    public Text _save7;
    public Text _save8;
    private static int number;
    private static int number1;
    private static int number2;
    private static int number3;
    private static int number4;
    private static int number5;
    private static int number6;
    private static int number7;
    private static int number8;
    private static DateTime dt1;
    private static DateTime dt2;
    private static DateTime dt3;
    private static DateTime dt4;
    private static DateTime dt5;
    private static DateTime dt6;
    private static DateTime dt7;
    private static DateTime dt8;
    private static int[]count = { 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject savePanel;


    // Start is called before the first frame update
    void Start()
    {
        save = PlayerPrefs.GetInt("SAVE");
        PlayerPrefs.SetInt("SAVE", 0);
        number = PlayerPrefs.GetInt("NUMBER");
        heroinname = PlayerPrefs.GetString("INPUTNAME");
        //ここに飛びたいストーリー番号を書いてsave画面から始める　例  number = 34;
        if (count[0] == 0)
        {
            _save1.text = "セーブできるよ";
        } else
        {
            _save1.text = number1.ToString("000") + "日付" + dt1;
        }
        if (count[1] == 0)
        {
            _save2.text = "セーブできるよ";
        } else
        {
            _save2.text = number2.ToString("000") + "日付" + dt2;
        }
        if (count[2] == 0)
        {
            _save3.text = "セーブできるよ";
        } else
        {
            _save3.text = number3.ToString("000") + "日付" + dt3;
        }
        if (count[3] == 0)
        {
            _save4.text = "セーブできるよ";
        } else
        {
            _save4.text = number4.ToString("000") + "日付" + dt4;
        }
        if (count[4] == 0)
        {
            _save5.text = "セーブできるよ";
        } else
        {
            _save5.text = number5.ToString("000") + "日付" + dt5;
        }
        if (count[5] == 0)
        {
            _save6.text = "セーブできるよ";
        } else
        {
            _save6.text = number6.ToString("000") + "日付" + dt6;
        }
        if (count[6] == 0)
        {
            _save7.text = "セーブできるよ";
        } else
        {
            _save7.text = number7.ToString("000") + "日付" + dt7;
        }
        if (count[7] == 0)
        {
            _save8.text = "セーブできるよ";
        } else
        {
            _save8.text = number8.ToString("000") + "日付" + dt8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

       
       
        
        
        
        

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

    public void save5_onClicked()
    {
        Save(ref number5, ref _save5, ref dt5, ref count[4], ref heroin[4]);
    }

    public void save6_onClicked()
    {
        Save(ref number6, ref _save6, ref dt6, ref count[5], ref heroin[5]);
    }

    public void save7_onClicked()
    {
        Save(ref number7, ref _save7, ref dt7, ref count[6], ref heroin[6]);
    }

    public void save8_onClicked()
    {
        Save(ref number8, ref _save8, ref dt8, ref count[7], ref heroin[7]);
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
            savetext.text = "記録されました" + savenum;
            load = 0;
            SceneManager.LoadScene("start scene");
        }
        else if (load == 1)
        {
            if (savecount == 1)
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
            } else
            {
                savetext.text = "セーブデータがないよ";
                
            }


        }
    }
    public void load_onClicked()
    {
        save = 0;
        load = 1;
        Invoke("jumptoSave", 0.52f);
        //SceneManager.LoadScene("save scene");
    }

    public void jumptoSave()
    {
        SceneManager.LoadScene("save scene");
    }

    public void onClicked_next()
    {
        savePanel.SetActive(true);
    }
    public void onClicked_back()
    {
        savePanel.SetActive(false);
    }

    public void onClicked_startback()
    {
        load = 0;
        SceneManager.LoadScene("start scene");
    }
 }
