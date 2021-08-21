using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Qdata;

public class Story : MonoBehaviour
{
    private Text _story; //ストーリーテキスト
    private Text _name;
    private Text _inputName;
    private Text _monthtext;
    private Text _logtext;
    public Text _selectbuttontext3;
    public Text _selectbuttontext1;
    public Text _selectbuttontext2;
    private AudioSource Soundbgm; //bgm
    public GameObject charactercenter;
    public GameObject characterright;
    public GameObject characterleft;
    public GameObject background;
    public GameObject textbox;
    public GameObject still;
    public GameObject month;
    public Sprite ouziSprite;
    public Sprite rukiaSprite;
    public Sprite hikaruSprite;
    public Sprite clearSprite;
    public Sprite statueSprite;
    public Sprite ouziojiSprite;
    public Sprite rukiaojiSprite;
    public Sprite hikaruojiSprite;
    public Sprite ouzismileSprite;
    public Sprite hikarutroubleSprite;
    public Sprite ouziojismileSprite;
    public Sprite back_classroomSprite;
    public Sprite back_stairsSprite;
    public Sprite back_corridorSprite;
    public Sprite back_gardenSprite;
    public Sprite back_schoolSprite;
    public Sprite back_dispensarySprite;
    public Sprite back_shoploadSprite;
    public Sprite back_fancyshopSprite;
    public Sprite back_May;
    public Sprite blackSprite;
    public Sprite still_clearSprite;
    public Sprite still_AprilSprite;
    public Sprite still_MaySprite;
    public Sprite month_MaySprite;
    public Sprite month_clearSprite;
    public Sprite text_ouzi;
    public Sprite text_hikaru;
    public Sprite text_rukia;
    public Sprite text_mob;
    private string centersr;
    private string rightsr;
    private string leftsr;
    private string backsr;
    private string stillsr;
    private string colorsr;
    private string bgm_state_sr;
    private string bgm_num_sr;
    private string se_num_sr;
    private string selectdisp_sr;
    private string selectbutton_num_sr;
    private string monthsr;
    private string textcolorsr;
    private String heroineName;
    private String logheroineName;
    private String characternamelength;
    private String logcharacterName;
    private String logstory;

    public static int selectpanelState;
    public static int index_read; //読み取り用
    public static int index_skip; //skip用

    
    

    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;

    public AudioClip[] cv;

    public AudioClip se1;
    public AudioClip Jingle;

    AudioSource[] sounds;

    GameObject _Screenbutton;//button
    public TextAsset storyText; //csvストーリーデータ
    private string _storyArray;
    private List<Qdata> _qdataList = new List<Qdata>();

    //logのcsv用
    public TextAsset logstoryText; //csvlogstorytextデータ
    private string _logstoryArray;
    private List<Logdata> _logdataList = new List<Logdata>();
    int logNum = 0; //story数
    //int logtextprintcount = 0;
    public string logspace = "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0" + "\u00A0";
    //log用終わり


    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数
    public int savenum = 0;
    public int menucount = 0;
    public int nameinput = 0;
    public float novelSpeed; //表示の速さ
    private int click = 0;

    [SerializeField] GameObject ScreenButton;
    [SerializeField] GameObject SelectButtonPanel;
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject MenuPanel;

    [SerializeField] GameObject SelectButton_3;

    private int selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        //BGM初期状態
        sounds = GetComponents<AudioSource>();
        sounds[0].clip = bgm1;
        sounds[0].Play();

        //選択肢パネル非表示
        SelectButtonPanel.SetActive(false);

        logPanel.SetActive(true);

        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();
        _monthtext = GameObject.Find("monthtext").GetComponent<Text>();
        _logtext = GameObject.Find("logtext").GetComponent<Text>();

        //logパネル非表示
        logPanel.SetActive(false);

        //_inputName = GameObject.Find("InputField").GetComponent<Text>();
        _Screenbutton = GameObject.Find("Screenbutton");

        PlayerPrefs.SetInt("SAVE", 0);
        PlayerPrefs.Save();

        qstory = PlayerPrefs.GetInt("NUMBERLOAD");
        nameinput = PlayerPrefs.GetInt("NAMEINPUT");
        
        if(nameinput == 0)
        {
            heroineName = PlayerPrefs.GetString("INPUTNAME");
            Debug.Log("名前" + heroineName);
        } else
        {
            heroineName = PlayerPrefs.GetString("INPUTNAME2");
            //Debug.Log("名前だよ" + heroineName);
        }

        heroineName = PlayerPrefs.GetString("INPUTNAME");
        logheroineName = heroineName;
        int namelength = System.Text.Encoding.GetEncoding(932).GetByteCount(heroineName);
        //Debug.Log(heroineName);
        if(namelength != 12)
        {
            int spacecount = 12 - namelength;
            for (int i = 0; i < spacecount; i++)
            {
                logheroineName += "\u00A0";
            }
            //Debug.Log(logheroineName);
        }

        //csvファイルからテキストを読み込み
        _storyArray = storyText.text.Replace(" ", "\u00A0");
        StringReader sr = new StringReader(_storyArray);
        sr.ReadLine();
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            _qdataList.Add(new Qdata(line));
            qNum++;           
        }
        //print("A");
          //最初のストーリーをセット
        //確認のためにConsoleに出力
        foreach (Qdata q in _qdataList)
        {
            //q.WriteDebugLog();
        }

        //log用
        //log用のcsvファイルからテキストを読み込み
        _logstoryArray = logstoryText.text.Replace(" ", "");
        //_logstoryArray = logstoryText.text.Replace("?", "\u0030");
        StringReader logr = new StringReader(_logstoryArray);
        logr.ReadLine();
        while (logr.Peek() > -1)
        {
            string line = logr.ReadLine();
            _logdataList.Add(new Logdata(line));
            logNum++;
        }
        foreach (Logdata q in _logdataList)
        {
            q.WriteDebugLog_logstory();
        }
        //

        StartCoroutine(Novel(qstory++));


    }

    private IEnumerator Novel(int index)
    {
        //skip用
        index_read = index;
        if(SkipselectPanelScript.clicked_skip == true) {
            if(SkipselectPanelScript.first == true)
            {
                index_read = index;
                index = index_skip;
                //qstory = index_skip;
                SkipselectPanelScript.first = false;
            }
            else if(SkipselectPanelScript.first == false)
            {
                index_skip++;
                index = index_skip;
                index_read = index;
            }
            if(index == 90)
            {
                index_read = index;
                SkipselectPanelScript.clicked_skip = false;
            }
        }
        Debug.Log(index_skip);

        int messageCount = 0; //表示中の文字数
        _story.text = "";

        //SE
        se_num_sr = _qdataList[index].se_num;
        if (int.Parse(se_num_sr) == 1)
        {
            sounds[1].PlayOneShot(se1);
        }
        if (int.Parse(se_num_sr) == 2)
        {
            sounds[1].PlayOneShot(Jingle);
        }

        //CV
        //Debug.Log(index);
        sounds[2].PlayOneShot(cv[index]);

        //BGM種類
        bgm_num_sr = _qdataList[index].bgm_num;
        if (int.Parse(bgm_num_sr) == 1)
        {
            sounds[0].clip = bgm1;
        }
        if (int.Parse(bgm_num_sr) == 2)
        {
            sounds[0].clip = bgm2;
        }
        if (int.Parse(bgm_num_sr) == 3)
        {
            sounds[0].clip = bgm3;
        }

        //BGM状態
        bgm_state_sr = _qdataList[index].bgm_state;
        if (int.Parse(bgm_state_sr) == 1)
        {
            sounds[0].Play();
        }
        if (int.Parse(bgm_state_sr) == 2)
        {
            sounds[0].Stop();
        }

        SelectButton_3.SetActive(true);

        //選択肢が３つ以外の時はSelectButton_3を表示しない
        selectbutton_num_sr = _qdataList[index].selectbutton_num;
        //Debug.Log("selectbutton_num_sr: " + selectbutton_num_sr);
        if (int.Parse(selectbutton_num_sr) != 3)
        {
            //Debug.Log(selectbutton_num_sr);
            SelectButton_3.SetActive(false);
        }

        //選択肢パネル表示
        selectdisp_sr = _qdataList[index].selectdisp;
        selectpanelState = 0;
        if (int.Parse(selectdisp_sr) == 1)
        {
            selectpanelState = 1;
            novelSpeed = 0;
            ScreenButton.SetActive(false);
            SelectButtonPanel.SetActive(true);
            _selectbuttontext3.text = _qdataList[index].selectbuttontext3;
            _selectbuttontext1.text = _qdataList[index].selectbuttontext1;
            _selectbuttontext2.text = _qdataList[index].selectbuttontext2;
            if (selected == 0)
            {
                SelectButtonPanel.SetActive(true);
                ScreenButton.SetActive(false);
            }
        }


        //一枚絵
        stillsr = _qdataList[index].stillimage;
        Image stillimage = (Image)still.GetComponent<Image>();
        if (int.Parse(stillsr) == 0)
        {
            stillimage.sprite = still_clearSprite;
        }
        else if (int.Parse(stillsr) == 4)
        {
            stillimage.sprite = still_AprilSprite;
        }
        else if (int.Parse(stillsr) == 5)
        {
            stillimage.sprite = still_MaySprite;
        }

        //月のはじめの画像
        monthsr = _qdataList[index].monthimage;
        Image monthimage = (Image)month.GetComponent<Image>();
        if (int.Parse(monthsr) == 0)
        {
            monthimage.sprite = month_clearSprite;
            month.SetActive(false);
        }
        if (int.Parse(monthsr) == 5)
        {
            monthimage.sprite = month_MaySprite;
            month.SetActive(true);
        }

        //背景
        backsr = _qdataList[index].backimage;
        Image backimage = (Image)background.GetComponent<Image>();
        if (int.Parse(backsr) == 1)
        {
            backimage.sprite = back_corridorSprite;
        }
        else if (int.Parse(backsr) == 2)
        {
            backimage.sprite = back_stairsSprite;
        }
        else if (int.Parse(backsr) == 0)
        {
            backimage.sprite = back_classroomSprite;
        }
        else if (int.Parse(backsr) == 3)
        {
            backimage.sprite = back_gardenSprite;
        }
        else if (int.Parse(backsr) == 4)
        {
            backimage.sprite = back_schoolSprite;
        }
        else if (int.Parse(backsr) == 5)
        {
            backimage.sprite = back_dispensarySprite;
        }
        else if (int.Parse(backsr) == 24)
        {
            backimage.sprite = blackSprite;
        }
        else if (int.Parse(backsr) == 30)
        {
            backimage.sprite = still_AprilSprite;
        }
        else if(int.Parse(backsr) == 31)
        {
            backimage.sprite = still_MaySprite;
        }
        else if(int.Parse(backsr) == 8) {
            backimage.sprite = back_shoploadSprite;
        }
        else if(int.Parse(backsr) == 9)
        {
            backimage.sprite = back_fancyshopSprite;
        }



        //センター画像
        centersr = _qdataList[index].centerimage;
        Image centerCharacter = (Image)charactercenter.GetComponent<Image>();
        if (int.Parse(centersr) == 1)
        {
            centerCharacter.sprite = ouziSprite;
        } else if (int.Parse(centersr) == 2)
        {
            centerCharacter.sprite = rukiaSprite;
        }
        else if (int.Parse(centersr) == 0)
        {
            centerCharacter.sprite = clearSprite;
        }
        else if (int.Parse(centersr) == 3)
        {
            centerCharacter.sprite = hikaruSprite;
        }
        else if (int.Parse(centersr) == 4)
        {
            centerCharacter.sprite = ouziojiSprite;
        }
        else if (int.Parse(centersr) == 5)
        {
            centerCharacter.sprite = rukiaojiSprite;
        }
        else if (int.Parse(centersr) == 6)
        {
            centerCharacter.sprite = hikaruojiSprite;
        }
        else if (int.Parse(centersr) == 7)
        {
            centerCharacter.sprite = ouzismileSprite;
        }
        else if(int.Parse(centersr) == 8)
        {
            centerCharacter.sprite = hikarutroubleSprite;
        }
        else if (int.Parse(centersr) == 24)
        {
            centerCharacter.sprite = statueSprite;
        }

        //ライト画像
        rightsr = _qdataList[index].rightimage;
        Image rightCharacter = (Image)characterright.GetComponent<Image>();
        if (int.Parse(rightsr) == 1)
        {
            rightCharacter.sprite = ouziSprite;
        }
        else if (int.Parse(rightsr) == 2)
        {
            rightCharacter.sprite = rukiaSprite;
        }
        else if (int.Parse(rightsr) == 0)
        {
            rightCharacter.sprite = clearSprite;
        }
        else if (int.Parse(rightsr) == 3)
        {
            rightCharacter.sprite = hikaruSprite;
        }
        else if (int.Parse(rightsr) == 4)
        {
            rightCharacter.sprite = ouziojiSprite;
        }
        else if (int.Parse(rightsr) == 5)
        {
            rightCharacter.sprite = rukiaojiSprite;
        }
        else if (int.Parse(rightsr) == 6)
        {
            rightCharacter.sprite = hikaruojiSprite;
        }
        else if (int.Parse(centersr) == 7)
        {
            rightCharacter.sprite = ouzismileSprite;
        }
        else if (int.Parse(centersr) == 8)
        {
            rightCharacter.sprite = hikarutroubleSprite;
        }
        else if (int.Parse(rightsr) == 24)
        {
            rightCharacter.sprite = statueSprite;
        }

        //レフト画像
        leftsr = _qdataList[index].leftimage;
        Image leftCharacter = (Image)characterleft.GetComponent<Image>();
        if (int.Parse(leftsr) == 1)
        {
            leftCharacter.sprite = ouziSprite;
        }
        else if (int.Parse(leftsr) == 2)
        {
            leftCharacter.sprite = rukiaSprite;
        }
        else if (int.Parse(leftsr) == 0)
        {
            leftCharacter.sprite = clearSprite;
        }
        else if (int.Parse(leftsr) == 3)
        {
            leftCharacter.sprite = hikaruSprite;
        }
        else if (int.Parse(leftsr) == 4)
        {
            leftCharacter.sprite = ouziojiSprite;
        }
        else if (int.Parse(leftsr) == 5)
        {
            leftCharacter.sprite = rukiaojiSprite;
        }
        else if (int.Parse(leftsr) == 6)
        {
            leftCharacter.sprite = hikaruojiSprite;
        }
        else if (int.Parse(centersr) == 7)
        {
            leftCharacter.sprite = ouzismileSprite;
        }
        else if (int.Parse(centersr) == 8)
        {
            leftCharacter.sprite = hikarutroubleSprite;
        }
        else if (int.Parse(leftsr) == 24)
        {
            leftCharacter.sprite = statueSprite;
        }
        //画像の色
        colorsr = _qdataList[index].charactercolor;
        if(int.Parse(colorsr) == 0)
        {
            centerCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            rightCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            leftCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }else if(int.Parse(colorsr) == 1)
        {
            centerCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            rightCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            leftCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }else if(int.Parse(colorsr) == 2)
        {
            centerCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            rightCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            leftCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }
        else
        {
            centerCharacter.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            rightCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            leftCharacter.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        }

        //テキストボックスの色
        textcolorsr= _qdataList[index].textcolor;
        Image textboximage = (Image)textbox.GetComponent<Image>();
        if (int.Parse(textcolorsr) == 1)
        {
            textboximage.sprite = text_ouzi;
        }
        else if (int.Parse(textcolorsr) == 2)
        {
            textboximage.sprite = text_rukia;
        }
        else if (int.Parse(textcolorsr) == 3)
        {
            textboximage.sprite = text_hikaru;
        }
        else if (int.Parse(textcolorsr) == 0 || int.Parse(textcolorsr) == 4)
        {
            textboximage.sprite = text_mob;
        }

        //名前
        if (int.Parse(textcolorsr) == 0)
        {
            _name.text = heroineName;
        }
        else
        {
            _name.text = _qdataList[index].nameText;
        }
        
        //左上の月の表示
        if(index < 50)
        {
            _monthtext.text = "4月";
        } else if(index < 55)
        {
            _monthtext.text = "5月";
        }

        //ストーリーテキスト表示
        while (_qdataList[index].storyText.Length > messageCount)
        {
            _story.text += _qdataList[index].storyText[messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelSpeed);
        }
        if(_qdataList[index].storyText.Length == messageCount)
        {
            click = 1;
            novelSpeed = 0.1f;
            //全文表示され終わったらlogにテキストを追加
            if(_qdataList[index].nameText == "えみ")
            {
                _logtext.text += logheroineName;
            } else
            {
                int characternamelength = System.Text.Encoding.GetEncoding(932).GetByteCount(_qdataList[index].nameText);
                logcharacterName = _qdataList[index].nameText;
                if (characternamelength != 12)
                {
                    int spacecount = 12 - characternamelength;
                    for (int i = 0; i < spacecount; i++)
                    {
                        logcharacterName += "\u00A0";
                    }
                }
                _logtext.text += logcharacterName;
            }
            
            _logtext.text += _logdataList[index].logstorytext1;
            _logtext.text += "\n";
            if(_logdataList[index].logstorytext2 != "0")
            {
                _logtext.text += logspace;
                _logtext.text += _logdataList[index].logstorytext2;
                _logtext.text += "\n";
            }

            if (_logdataList[index].logstorytext3 != "0")
            {
                _logtext.text += logspace;
                _logtext.text += _logdataList[index].logstorytext3;
                _logtext.text += "\n";
            }

            if (_logdataList[index].logstorytext4 != "0")
            {
                _logtext.text += logspace;
                _logtext.text += _logdataList[index].logstorytext4;
                _logtext.text += "\n";
            }

            if (_logdataList[index].logstorytext5 != "0")
            {
                _logtext.text += logspace;
                _logtext.text += _logdataList[index].logstorytext5;
                _logtext.text += "\n";
            }

            if (_logdataList[index].logstorytext6 != "0")
            {
                _logtext.text += logspace;
                _logtext.text += _logdataList[index].logstorytext6;
                _logtext.text += "\n";
            }
            
            //_logtext.text += logspace;
            //_logtext.text += _logdataList[index].logstorytext3;
            //_logtext.text += "\n";
            //_logtext.text += logspace;
            //_logtext.text += _logdataList[index].logstorytext4;
            //_logtext.text += "\n";
            //_logtext.text += logspace;
            //_logtext.text += _logdataList[index].logstorytext5;
            //_logtext.text += "\n";
            //_logtext.text += logspace;
            //_logtext.text += _logdataList[index].logstorytext6;
            _logtext.text += "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Menu();
    }

    private void Menu()
    {
        //Debug.Log(menucount);
        if (MenuPanel.activeSelf == true)
        {
            menucount++;
        } else if(MenuPanel.activeSelf == false)
        {
            if(menucount != 0)
            {
                menucount--;
            }
        }
        if (Input.GetKey(KeyCode.M) && MenuPanel.activeSelf == false && menucount == 0)
        {
            savenum = 1;
            PlayerPrefs.SetInt("SAVE", savenum);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("NUMBER", qstory);
            PlayerPrefs.Save();
            MenuPanel.SetActive(true);
        } else if(Input.GetKey(KeyCode.M) && MenuPanel.activeSelf == true && menucount > 200)
        {
            menucount = 200;
            MenuPanel.SetActive(false);
        }

    }

    public void onClick_Screenbutton()
    {
        if(click == 0)
        {
            novelSpeed = 0;
        }
        if (qNum > qstory && click == 1)
        {
            sounds[2].Stop();
            //StartCoroutine(Novel(qstory++));
            //click = 0;
            
            //if (selected != 0) {
            //    selected = 0;
            //}
            if(!(selected == 1 && qstory == 91))
            {
                StartCoroutine(Novel(qstory++));
                click = 0;
            }
        }
    }

    //選択肢ボタン選択時の動き
    public void onClick_SelectButton_3()
    {
        selected = 3;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_1()
    {
        selected = 1;
        //qstoryは調整する
        qstory = 70 - 2;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_2()
    {
        selected = 2;
        //qstoryは調整する
        qstory = 93 - 2;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }
}

//質問を管理するクラス
public class Qdata
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

    public Qdata(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 19)
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
        }
    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t" + selectbuttontext1 + "\t" + selectbuttontext2);
    }

    public class Logdata
    {
        int textnumber;
        public string logstorytext1;
        public string logstorytext2;
        public string logstorytext3;
        public string logstorytext4;
        public string logstorytext5;
        public string logstorytext6;
        int lengthcount;
        public string mainstory;
        public int check;
        public int nokori1;
        public int nokori2;
        public int nokori3;
        public int nokori4;
        public int nokori5;
        public int nokori6;

        public Logdata(string txt)
        {
            string[] spTxt = txt.Split(',');
            if (spTxt.Length == 16)
            {
                textnumber = int.Parse(spTxt[0]);
                logstorytext1 = spTxt[1];
                logstorytext2 = spTxt[2];
                logstorytext3 = spTxt[3];
                logstorytext4 = spTxt[4];
                logstorytext5 = spTxt[5];
                logstorytext6 = spTxt[6];
                lengthcount = int.Parse(spTxt[7]);
                mainstory = spTxt[8];
                check = int.Parse(spTxt[9]);
                nokori1 = int.Parse(spTxt[10]);
                nokori2 = int.Parse(spTxt[11]);
                nokori3 = int.Parse(spTxt[12]);
                nokori4 = int.Parse(spTxt[13]);
                nokori5 = int.Parse(spTxt[14]);
                nokori6 = int.Parse(spTxt[15]);
            }
        }

        public void WriteDebugLog_logstory()
        {
            //Debug.Log(logstorytext1 + "\t" + logstorytext2 + "\t" + logstorytext3 + "\t" + logstorytext4 + "\t" + logstorytext5 + "\t" + logstorytext6);
        }
    }

}