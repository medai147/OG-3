using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Story_new : MonoBehaviour
{

    //仕様メモ
    /*
     * スキップは次の月または選択肢
     */

    public static Story_new instance;
    
    
    public bool textnextflag = false;
    private bool automodeflag = false;

    bool textread = false; //文字再生中
    public bool animationfinishedflag = true;

    private Text _story; //ストーリーテキスト
    private Text _name;

    private Text _move; //移動アニメーション時のテキスト


    public TextAsset storyText; //csvストーリーデータ
    private string _storyArray;
    private List<Qdata_new> _qdataList = new List<Qdata_new>();

    AudioClip oldbgmClip = null;
    AudioClip bgmClip = null;
    AudioClip seClip = null;


    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数
    int messageCount = 0; //表示されている文字の数
    float novelspeed = 0.1f; //文字の表示速度

    public int nameinput = 0;
    private String heroineName;

    AudioSource[] sounds;

    [SerializeField] GameObject textbox;
    [SerializeField] GameObject ScreenButton;
    [SerializeField] GameObject SelectButtonPanel;
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject cannotskipAlertPanel;
    [SerializeField] GameObject LoadingPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject Selectbutton_1;
    [SerializeField] GameObject Selectbutton_2;
    [SerializeField] GameObject SelectButton_3;
    [SerializeField] GameObject menubutton;
    [SerializeField] GameObject monthtext;
    [SerializeField] GameObject monthTextPanel;

    [SerializeField] GameObject moveanimationPrefab;
    [SerializeField] GameObject fadecloseanimationPrefab;
    [SerializeField] GameObject fadeopenanimationPrefab;

    [SerializeField] GameObject[] autosetbutton = new GameObject[5];

    [SerializeField] GameObject[] textsetbutton = new GameObject[5];

    [SerializeField] GameObject[] sesetbutton = new GameObject[5];

    [SerializeField] GameObject[] bgmsetbutton = new GameObject[5];

    [SerializeField] Auto_newScript autoscript;



    // Start is called before the first frame update

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //オーディオソース
        sounds = GetComponents<AudioSource>();
        sounds[0].Play();

        //テキスト
        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();

        nameinput = PlayerPrefs.GetInt("NAMEINPUT");
        if (nameinput == 0)
        {
            heroineName = PlayerPrefs.GetString("INPUTNAME");
            //Debug.Log("名前" + heroineName);
        }
        else
        {
            heroineName = PlayerPrefs.GetString("INPUTNAME2");
            //Debug.Log("名前だよ" + heroineName);
        }

        heroineName = PlayerPrefs.GetString("INPUTNAME");

        //csvファイルからテキストを読み込み
        _storyArray = storyText.text.Replace(" ", "\u00A0");
        _storyArray = storyText.text.Replace("@", heroineName);
        StringReader sr = new StringReader(_storyArray);
        sr.ReadLine();
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            _qdataList.Add(new Qdata_new(line));
            qNum++;
        }
        //最初のストーリーをセット
        //確認のためにConsoleに出力
        foreach (Qdata_new q in _qdataList)
        {
            //q.WriteDebugLog();
        }


        //最初のスタートだから変更無し
        StartCoroutine(Novel(qstory));
    }

    // Update is called once per frame
    void Update()
    {
        //コルーチンを進める
        if ((textnextflag && animationfinishedflag))
        {
            //スピードが0から戻らないから代入した　後で設定画面で選んだ物に応じた値を入れるようにしたい
            novelspeed = 0.1f;
            StartCoroutine(Novel(qstory));
        }

        //オート中
        if (autoscript.autoflag && !textread)
        {
            //オートモードの速度ボタンによってここで停止処理
            textnextflag = true;
        }
    }

    private IEnumerator Novel(int index)
    {
        textnextflag = false;

        stilldisplay();
        monthstartdisplay();
        backdisplay();
        characterdisplay();
        charactercolor();
        textcolor();
        moveanimation();
        fadeanimation();
        playBGM();
        playSE();


        //名前にmonthが入っている場所を通過したらテキスト変更
        if (_qdataList[index].nameText.Contains("month"))
        {
            //左上の月表示
            GameObject.Find("monthtext").GetComponent<Text>().text = _qdataList[index].nameText.Replace("month", "");
        }

        //名前
        String textcolorsr = _qdataList[index].textcolor;
        if (textcolorsr.Equals("text_own"))
        {
            _name.text = heroineName;
        }
        else if (textcolorsr.Equals("text_monologue"))
        {
            _name.text = "";
        }
        else
        {
            _name.text = _qdataList[index].nameText;
        }


        //本文再生
        _story.text = "";
        messageCount = 0;
        while (_qdataList[index].storyText.Length > messageCount)
        {
            textread = true;
            _story.text += _qdataList[index].storyText[messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelspeed);
        }

        //ログを管理するリストに名前とテキストを代入(選択肢のところはまた考える)
        if(_name.text == "")
        {
            GameManager.instance.logtext.Add("\n" + _qdataList[index].storyText);
        } else
        {
            GameManager.instance.logtext.Add("\n" + _name.text + ":" + _qdataList[index].storyText);
        }
        
        textread = false;

        //ストーリー番号を次に進める
        qstory++;
    }

    public void onClicked_screenbutton()
    {
        if(!autoscript.autoflag)
        {
            if (textread)
            {
                novelspeed = 0;

            }
            else
            {
                textnextflag = true;
            }
        }

    }

    public void onClicked_automodebutton()
    {
        //オートモードスタート
        automodeflag = true;
    }

    private void stilldisplay()
    {
        //一枚絵
        String stillsr = _qdataList[qstory].stillimage;
        Image stillimage = (Image)GameObject.Find("stillImage").GetComponent<Image>();
        if (stillsr.Equals("0"))
        {
            menubutton.SetActive(true);
            stillimage.sprite = Resources.Load<Sprite>("Sprites/back/back_clear");
        }
        else
        {
            String getstill_index = Regex.Replace(stillsr, @"[^0-9]", "");
            GameManager.instance.getimage[int.Parse(getstill_index)] = 1;
            stillimage.sprite = Resources.Load<Sprite>("Sprites/back/" + stillsr.Replace(getstill_index, ""));
            menubutton.SetActive(false);
        }
    }

    private void monthstartdisplay()
    {
        //月のはじめの画像
        String monthsr = _qdataList[qstory].monthimage;
        Image monthimage = (Image)GameObject.Find("month").GetComponent<Image>();
        if (monthsr.Equals("0"))
        {
            monthimage.sprite = Resources.Load<Sprite>("Sprites/back/back_clear");
        } else
        {
            monthimage.sprite = Resources.Load<Sprite>("Sprites/month/" + monthsr);
        }
    }

    private void backdisplay()
    {
        //背景
        String backsr = _qdataList[qstory].backimage;
        Image backimage = (Image)GameObject.Find("backgroundImage").GetComponent<Image>();
        backimage.sprite = Resources.Load<Sprite>("Sprites/back/" + backsr);
    }

    private void characterdisplay()
    {
        //センター画像
        String centersr = _qdataList[qstory].centerimage;
        Image centerCharacter = (Image)GameObject.Find("CenterCharacterImages").GetComponent<Image>();
        centerCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + centersr);


        //ライト画像
        String rightsr = _qdataList[qstory].rightimage;
        Image rightCharacter = (Image)GameObject.Find("RightCharacterImages").GetComponent<Image>();
        rightCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + rightsr);


        //レフト画像
        String leftsr = _qdataList[qstory].leftimage;
        Image leftCharacter = (Image)GameObject.Find("LeftCharacterImages").GetComponent<Image>();
        leftCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + leftsr);
    }

    private void charactercolor() {
        //画像の色
        String colorsr = _qdataList[qstory].charactercolor;
        if (int.Parse(colorsr) == 0)
        {
            GameObject.Find("CenterCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("RightCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("LeftCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (int.Parse(colorsr) == 1)
        {
            GameObject.Find("CenterCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            GameObject.Find("RightCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            GameObject.Find("LeftCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (int.Parse(colorsr) == 2)
        {
            GameObject.Find("CenterCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            GameObject.Find("RightCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("LeftCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else
        {
            GameObject.Find("CenterCharacterImages").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            GameObject.Find("RightCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            GameObject.Find("LeftCharacterImages").GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
    }

    private void textcolor()
    {
        //テキストボックスの色
        String textcolorsr = _qdataList[qstory].textcolor;
        Image textboximage = (Image)GameObject.Find("TextImage").GetComponent<Image>();
        textboximage.sprite = Resources.Load<Sprite>("Sprites/UI/" + textcolorsr);
        monthTextPanel.SetActive(true);
        if (textcolorsr.Equals("0"))
        {
            textboximage.sprite = Resources.Load<Sprite>("Sprites/back/back_clear");
            monthTextPanel.SetActive(false);
        }
    }

    private void moveanimation()
    {
        String moveanimationtext = _qdataList[qstory].moveanimation;
        if (!moveanimationtext.Equals("0"))
        {
            Instantiate(moveanimationPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity,GameObject.Find("BackgroundPanel").transform);
            _move = GameObject.Find("movetext").GetComponent<Text>();
            _move.text = moveanimationtext;
            animationfinishedflag = false;
        } else
        {
            animationfinishedflag = true;
        }
    }

    private void fadeanimation()
    {
        String fadeflag = _qdataList[qstory].fadeanimation;
        if (int.Parse(fadeflag) == 1)
        {
            Instantiate(fadecloseanimationPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, GameObject.Find("GameManager").transform);
            animationfinishedflag = false;
        }
    }

    private void playBGM()
    {
        String bgm_sr = _qdataList[qstory].bgm;
        if(!bgm_sr.Equals("0"))
        {
            bgmClip = Resources.Load<AudioClip>("AudioClips/" + bgm_sr);
            if (oldbgmClip != bgmClip)
            {
                sounds[0].clip = bgmClip;
                sounds[0].Play();
            }
            oldbgmClip = bgmClip;
        } else
        {
            sounds[0].Stop();
        }
    }

    private void playSE()
    {
        String se_sr = _qdataList[qstory].se;
        if (!se_sr.Equals("0"))
        {
            seClip = Resources.Load<AudioClip>("AudioClips/" + se_sr);

            sounds[1].PlayOneShot(seClip);
        }
    }
}

//質問を管理するクラス
public class Qdata_new
{
    int number;
    public string storyText;
    public string nameText;
    public string centerimage;
    public string rightimage;
    public string leftimage;
    public string backimage;
    public string stillimage;
    public string charactercolor;
    public string bgm;
    public string se;
    public string selectdisp;
    public string selectbutton_num;
    public string monthimage;
    public string selectbuttontext3;
    public string selectbuttontext1;
    public string selectbuttontext2;
    public string textcolor;
    public string fadeanimation;
    public string moveanimation;


    public Qdata_new(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 20)
        {
            number = int.Parse(spTxt[0]);
            storyText = spTxt[1];
            nameText = spTxt[2];
            centerimage = spTxt[3];
            rightimage = spTxt[4];
            leftimage = spTxt[5];
            backimage = spTxt[6];
            stillimage = spTxt[7];
            charactercolor = spTxt[8];
            bgm = spTxt[9];
            se = spTxt[10];
            selectdisp = spTxt[11];
            selectbutton_num = spTxt[12];
            monthimage = spTxt[13];
            selectbuttontext3 = spTxt[14];
            selectbuttontext1 = spTxt[15];
            selectbuttontext2 = spTxt[16];
            textcolor = spTxt[17];
            fadeanimation = spTxt[18];
            moveanimation = spTxt[19];
        }
    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t" + selectbuttontext1 + "\t" + selectbuttontext2);
    }

}