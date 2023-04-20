using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Story_new : MonoBehaviour
{
    public bool textnextflag = false;
    private bool automodeflag = false;
    bool textread = false; //文字再生中

    private Text _story; //ストーリーテキスト
    private Text _name;


    public TextAsset storyText; //csvストーリーデータ
    private string _storyArray;
    private List<Qdata_new> _qdataList = new List<Qdata_new>();


    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数
    int messageCount = 0; //表示されている文字の数
    float novelspeed = 0.1f; //文字の表示速度

    public int nameinput = 0;
    private String heroineName;

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
    [SerializeField] GameObject movePanel;

    [SerializeField] GameObject[] autosetbutton = new GameObject[5];

    [SerializeField] GameObject[] textsetbutton = new GameObject[5];

    [SerializeField] GameObject[] sesetbutton = new GameObject[5];

    [SerializeField] GameObject[] bgmsetbutton = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        //テキスト
        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();
        //_monthtext = GameObject.Find("monthtext").GetComponent<Text>();
        //_logtext = GameObject.Find("logtext").GetComponent<Text>();

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
        StartCoroutine(Novel(qstory++));
    }

    // Update is called once per frame
    void Update()
    {
        if (textnextflag)
        {
            Debug.Log("a");
            StartCoroutine(Novel(qstory++));
        }
    }

    private IEnumerator Novel(int index)
    {
        textnextflag = false;
        //オート中
        if(automodeflag)
        {
            //オートモードの速度ボタンによってここで停止処理
            textnextflag = true;
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
        while (_qdataList[index].storyText.Length > messageCount)
        {
            textread = true;
            _story.text += _qdataList[index].storyText[messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelspeed);
        }
        textread = false;


    }

    public void onClicked_screenbutton()
    {
        if(textread)
        {
            novelspeed = 0;
        } else
        {
            textnextflag = true;
        }
    }

    public void onClicked_automodebutton()
    {
        //オートモードスタート
        automodeflag = true;
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
    public string bgm_state;
    public string bgm_num;
    public string se_num;
    public string selectdisp;
    public string selectbutton_num;
    public string monthimage;
    public string selectbuttontext3;
    public string selectbuttontext1;
    public string selectbuttontext2;
    public string textcolor;
    public string animation;
    public string moveanimation;


    public Qdata_new(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 21)
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
            bgm_state = spTxt[9];
            bgm_num = spTxt[10];
            se_num = spTxt[11];
            selectdisp = spTxt[12];
            selectbutton_num = spTxt[13];
            monthimage = spTxt[14];
            selectbuttontext3 = spTxt[15];
            selectbuttontext1 = spTxt[16];
            selectbuttontext2 = spTxt[17];
            textcolor = spTxt[18];
            animation = spTxt[19];
            moveanimation = spTxt[20];
        }
    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t" + selectbuttontext1 + "\t" + selectbuttontext2);
    }

}