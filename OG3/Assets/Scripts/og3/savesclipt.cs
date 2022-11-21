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
    public Text _save1sentence;
    public Text _save2sentence;
    public Text _save3sentence;
    public Text _save4sentence;
    public Text _save5sentence;
    public Text _save6sentence;
    public Text _save7sentence;
    public Text _save8sentence;
    private static int number;
    private static int number1;
    private static int number2;
    private static int number3;
    private static int number4;
    private static int number5;
    private static int number6;
    private static int number7;
    private static int number8;
    private static string dt1;
    private static string dt2;
    private static string dt3;
    private static string dt4;
    private static string dt5;
    private static string dt6;
    private static string dt7;
    private static string dt8;
    private static int[]count = { 0, 0, 0, 0, 0, 0, 0, 0 };
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject savePanel;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        save = PlayerPrefs.GetInt("SAVE");
        PlayerPrefs.SetInt("SAVE", 0);
        number = PlayerPrefs.GetInt("NUMBER");
        heroinname = PlayerPrefs.GetString("INPUTNAME");

        count[0] = PlayerPrefs.GetInt("SAVECOUNT1");
        number1 = PlayerPrefs.GetInt("SAVEDATA1");
        heroin[0] = PlayerPrefs.GetString("SAVENAME1");
        dt1 = PlayerPrefs.GetString("SAVETIME1");

        count[1] = PlayerPrefs.GetInt("SAVECOUNT2");
        number2 = PlayerPrefs.GetInt("SAVEDATA2");
        heroin[1] = PlayerPrefs.GetString("SAVENAME2");
        dt2 = PlayerPrefs.GetString("SAVETIME2");

        count[2] = PlayerPrefs.GetInt("SAVECOUNT3");
        number3 = PlayerPrefs.GetInt("SAVEDATA3");
        heroin[2] = PlayerPrefs.GetString("SAVENAME3");
        dt3 = PlayerPrefs.GetString("SAVETIME3");

        count[3] = PlayerPrefs.GetInt("SAVECOUNT4");
        number4 = PlayerPrefs.GetInt("SAVEDATA4");
        heroin[3] = PlayerPrefs.GetString("SAVENAME4");
        dt4 = PlayerPrefs.GetString("SAVETIME4");

        count[4] = PlayerPrefs.GetInt("SAVECOUNT5");
        number5 = PlayerPrefs.GetInt("SAVEDATE5");
        heroin[4] = PlayerPrefs.GetString("SAVENAME5");
        dt5 = PlayerPrefs.GetString("SAVETIME5");

        count[5] = PlayerPrefs.GetInt("SAVECOUNT6");
        number6 = PlayerPrefs.GetInt("SAVEDATA6");
        heroin[5] = PlayerPrefs.GetString("SAVENAME6");
        dt6 = PlayerPrefs.GetString("SAVETIME6");

        count[6] = PlayerPrefs.GetInt("SAVECOUNT7");
        number7 = PlayerPrefs.GetInt("SAVEDATA7");
        heroin[6] = PlayerPrefs.GetString("SAVENAME7");
        dt7 = PlayerPrefs.GetString("SAVETIME7");

        count[7] = PlayerPrefs.GetInt("SAVECOUNT8");
        number8 = PlayerPrefs.GetInt("SAVEDATA8");
        heroin[7] = PlayerPrefs.GetString("SAVENAME8");
        dt8 = PlayerPrefs.GetString("SAVETIME8");


        //ここに飛びたいストーリー番号を書いてsave画面から始める　例  number = 34;
        if (count[0] == 0)
        {
            _save1.text = "セーブできるよ";
        } else
        {
            _save1.text = number1.ToString("000") + dt1;
            sentence(ref _save1sentence, number1);
        }
        if (count[1] == 0)
        {
            _save2.text = "セーブできるよ";
        } else
        {
            _save2.text = number2.ToString("000") + dt2;
            sentence(ref _save2sentence, number2);
        }
        if (count[2] == 0)
        {
            _save3.text = "セーブできるよ";
        } else
        {
            _save3.text = number3.ToString("000") + dt3;
            sentence(ref _save3sentence, number3);
        }
        if (count[3] == 0)
        {
            _save4.text = "セーブできるよ";
        } else
        {
            _save4.text = number4.ToString("000") + dt4;
            sentence(ref _save4sentence, number4);
        }
        if (count[4] == 0)
        {
            _save5.text = "セーブできるよ";
        } else
        {
            _save5.text = number5.ToString("000") + dt5;
            sentence(ref _save5sentence, number5);
        }
        if (count[5] == 0)
        {
            _save6.text = "セーブできるよ";
        } else
        {
            _save6.text = number6.ToString("000") + dt6;
            sentence(ref _save6sentence, number6);
        }
        if (count[6] == 0)
        {
            _save7.text = "セーブできるよ";
        } else
        {
            _save7.text = number7.ToString("000") + dt7;
            sentence(ref _save7sentence, number7);
        }
        if (count[7] == 0)
        {
            _save8.text = "セーブできるよ";
        } else
        {
            _save8.text = number8.ToString("000") + dt8;
            sentence(ref _save8sentence, number8);
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





    public void Save(ref int savenum, ref Text savetext,ref string dt,ref int savecount,ref string name)
    {
        if (load != 1)

        {
            savenum = number;
            dt = DateTime.Now.ToString();
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
                    //Image loadimage = (Image)loadstill.GetComponent<Image>();
                    //loadimage.sprite = loadsprite;
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
                    //Image loadimage = (Image)loadstill.GetComponent<Image>();
                    //loadimage.sprite = loadsprite;
                    PlayerPrefs.SetInt("NUMBERLOAD",savenum - 1);
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
        Debug.Log(load);
        if(load == 1)
        {
            SceneManager.LoadScene("start scene");
        } else if(load == 0)
        {
            if (number == 0)
            {
                loadPanel.SetActive(true);
                Image loadimage = (Image)loadstill.GetComponent<Image>();
                loadimage.sprite = loadsprite;
                PlayerPrefs.SetInt("NUMBERLOAD", number);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("NAMEINPUT", 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("INPUTNAME2", heroinname);
                PlayerPrefs.Save();
            }
            else
            {
                loadPanel.SetActive(true);
                Image loadimage = (Image)loadstill.GetComponent<Image>();
                loadimage.sprite = loadsprite;
                PlayerPrefs.SetInt("NUMBERLOAD",number - 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("NAMEINPUT", 1);
                PlayerPrefs.Save();
                PlayerPrefs.SetString("INPUTNAME2", heroinname);
                PlayerPrefs.Save();

            }
            SceneManager.LoadScene("Main scene");
        }
        load = 0;

    }

    public void sentence(ref Text sentence,int number)
    {
        if (number > 139)
        {
            sentence.text = "おうじくんとパンケーキを食べに行くことに！なんだか照れちゃうな";
        }
        else if (number > 107)
        {
            sentence.text = "おうじくんに誕生日プレゼントを渡すことに！";
        }
        else if (number > 99)
        {
            sentence.text = "家に帰ると誰かからSNSにフォローリクエストが来ていた";
        }
        else if (number > 68)
        {
            sentence.text = "光先輩と商店街に行くことに！もしかしてデート！？";
        }
        else if (number > 61 || 90 < number)
        {
            sentence.text = "光先輩に勉強を教えてもらうことに！近づくチャンスかも！";
        }
        else if (number > 44)
        {
            sentence.text = "目が覚めるとおじさん達に覗き込まれてた！";
        }
        else if (number > 27)
        {
            sentence.text = "転校初日に銅像にぶつかってしまった！ついてない日だな";
        }
        else if (number > 0)
        {
            sentence.text = "今日から新しい学校に通うの楽しみだな！";
        }
    }

    void OnActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        PlayerPrefs.SetInt("SAVEDATA1", number1);
        PlayerPrefs.SetString("SAVENAME1", heroin[0]);
        PlayerPrefs.SetInt("SAVECOUNT1", count[0]);
        PlayerPrefs.SetString("SAVETIME1", dt1.ToString());

        PlayerPrefs.SetInt("SAVEDATA2", number2);
        PlayerPrefs.SetString("SAVENAME2", heroin[1]);
        PlayerPrefs.SetInt("SAVECOUNT2", count[1]);
        PlayerPrefs.SetString("SAVETIME2", dt2.ToString());

        PlayerPrefs.SetInt("SAVEDATA3", number3);
        PlayerPrefs.SetString("SAVENAME3", heroin[2]);
        PlayerPrefs.SetInt("SAVECOUNT3", count[2]);
        PlayerPrefs.SetString("SAVETIME3", dt3.ToString());

        PlayerPrefs.SetInt("SAVEDATA4", number4);
        PlayerPrefs.SetString("SAVENAME4", heroin[3]);
        PlayerPrefs.SetInt("SAVECOUNT4", count[3]);
        PlayerPrefs.SetString("SAVETIME4", dt4.ToString());

        PlayerPrefs.SetInt("SAVEDATA5", number5);
        PlayerPrefs.SetString("SAVENAME5", heroin[4]);
        PlayerPrefs.SetInt("SAVECOUNT5", count[4]);
        PlayerPrefs.SetString("SAVETIME5", dt5.ToString());

        PlayerPrefs.SetInt("SAVEDATA6", number6);
        PlayerPrefs.SetString("SAVENAME6", heroin[5]);
        PlayerPrefs.SetInt("SAVECOUNT6", count[5]);
        PlayerPrefs.SetString("SAVETIME6", dt6.ToString());

        PlayerPrefs.SetInt("SAVEDATA7", number7);
        PlayerPrefs.SetString("SAVENAME7", heroin[6]);
        PlayerPrefs.SetInt("SAVECOUNT7", count[6]);
        PlayerPrefs.SetString("SAVETIME7", dt7.ToString());

        PlayerPrefs.SetInt("SAVEDATA8", number8);
        PlayerPrefs.SetString("SAVENAME8", heroin[7]);
        PlayerPrefs.SetInt("SAVECOUNT8", count[7]);
        PlayerPrefs.SetString("SAVETIME8", dt8.ToString());


    }
}
