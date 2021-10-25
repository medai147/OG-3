using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine.SceneManagement;
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
    public Sprite clearSprite;
    public Sprite ouziSprite;
    public Sprite rukiaSprite;
    public Sprite hikaruSprite;
    public Sprite ouziojiSprite;
    public Sprite rukiaojiSprite;
    public Sprite hikaruojiSprite;
    public Sprite ouzismileSprite;
    public Sprite hikarutroubleSprite;
    public Sprite ouziojismileSprite;
    public Sprite ouziojiangrySprite;
    public Sprite ouziojitroubleSprite;
    public Sprite rukiatroubleSprite;
    public Sprite rukiaojismileSprite;
    public Sprite statueSprite;
    //背景
    public Sprite back_classroomSprite;
    public Sprite back_corridorSprite;
    public Sprite back_stairsSprite;
    public Sprite back_gardenSprite;
    public Sprite back_schoolSprite;
    public Sprite back_dispensarySprite;
    public Sprite back_seaSprite;
    public Sprite back_groundSprite;
    public Sprite back_shoploadSprite;
    public Sprite back_fancyshopSprite;
    public Sprite back_cafeSprite;
    public Sprite back_heroineroom_noonSprite;
    public Sprite back_heroineroom_nightSprite;
    public Sprite back_garageSprite;
    public Sprite blackSprite;
    //スチル
    public Sprite still_clearSprite;
    public Sprite still_AprilSprite;
    public Sprite still_May_hikaruSprite;
    public Sprite still_June_ouziSprite;
    public Sprite still_July_rukiaSprite;
    public Sprite still_July_ouziSprite;
    public Sprite still_July_hikaruSprite;
    public Sprite still_August_hikaruSprite;
    public Sprite still_August_ouziSprite;
    //月
    public Sprite month_clearSprite;
    public Sprite month_MaySprite;
    public Sprite month_JuneSprite;
    public Sprite month_JulySprite;
    public Sprite month_AugustSprite;
    public Sprite month_SeptemberSprite;
    public Sprite month_OctoberSprite;
    public Sprite month_NovemberSprite;
    public Sprite month_DecemberSprite;
    public Sprite month_JanuarySprite;
    public Sprite month_FebruarySprite;

    //名前の色
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
    //private String characternamelength;
    private String logcharacterName;
    //private String logstory;

    public static int selectpanelState;
    public static int index_read; //読み取り用
    public static int index_skip; //skip用

    public static int startscene; //スタート画面の画像切り替え、ミニゲームボタン用

    
    

    public AudioClip bgm1; //ikemen_theme
    public AudioClip bgm2; //dark
    public AudioClip bgm3; //shock
    public AudioClip bgm4; //cafe

    public AudioClip[] cv;

    public AudioClip se1;
    public AudioClip Jingle;
    public AudioClip se2;

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
    public int automode = 0;
    public float novelSpeed; //表示の速さ
    private int click = 0;
    public int messageCount = 0;

    public float sevolume;
    public float bgmvolume;
    static int seMutecount = 0;
    static int bgmMutecount = 0;

    [SerializeField] GameObject ScreenButton;
    [SerializeField] GameObject SelectButtonPanel;
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject cannotskipAlertPanel;
    [SerializeField] GameObject LoadingPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject SelectButton_3;

    private int selected = 0;

    //設定画面
    public Image speedslide;
    static float speedbar_x;
    public Image seslide;
    public Image bgmslide;
    public Vector2 MousePos;
    public Canvas canvas;
    public RectTransform canvasRect;
    static int novelspeedcount = 4;
    static int sevolumecount = 4;
    static int bgmvolumecount = 4;
    static int autospeedcount = 4;
    public Image autoslide;
    static float autoslidebar_x;
    float time;
    bool autoclick;

    // Start is called before the first frame update
    void Start()
    {

        //設定画面用
        //speedslide = GameObject.Find("slideonimage").GetComponent<Image>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        if (startscene == 1)
        {
            //スタート画面の切り替え
            PlayerPrefs.SetInt("START", 1);
            PlayerPrefs.Save();
        }else
        {
            //スタート画面の切り替え
            PlayerPrefs.SetInt("START", 0);
            PlayerPrefs.Save();
        }


        //BGM初期状態
        sounds = GetComponents<AudioSource>();
        bgmvolume = 0.7f;
        sevolume = 0.7f;
        sounds[0].volume = bgmvolume;
        sounds[1].volume = sevolume;
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
        
        if (SkipselectPanelScript.clicked_skip == true) {
            if(SkipselectPanelScript.first == true)
            {
                LoadingPanel.SetActive(false);
                index_read = index;
                index = index_skip;
                qstory = index_skip;
                SkipselectPanelScript.first = false;
            }
            else if(SkipselectPanelScript.first == false)
            {
                index_skip++;
                qstory = index_skip;
                index = index_skip;
                index_read = index;
            }
            //if(index == 91 && selected == 1)
            //{
                //index_read = index;
                //index_skip = 94;
                //qstory = index_skip;
                //index = index_skip;
                //SkipselectPanelScript.clicked_skip = false;
            //}
        }

        //選択肢で飛んだ先から次の話に飛ぶところまとめ
        if (index == 91 && selected == 1) //ひかる買い物からおうじ誕プレへ飛ぶ
        {
            index_read = index;
            index_skip = 94;
            qstory = index_skip;
            index = index_skip;
        }

        if (index == 115 && selected == 3) //おうじ誕プレから６月へ飛ぶ
        {
            index_read = index;
            index_skip = 124;
            qstory = index_skip;
            index = index_skip;
        }
        if (index == 119 && selected == 1) //おうじ誕プレから６月へ飛ぶ
        {
            index_read = index;
            index_skip = 124;
            qstory = index_skip;
            index = index_skip;
        }
        if (index == 167 && selected == 1) //おうじパンケーキから７月へ飛ぶ
        {
            index_read = index;
            index_skip = 171;
            qstory = index_skip;
            index = index_skip;
        }

        //Debug.Log(index_read);

        messageCount = 0; //表示中の文字数
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
        if (int.Parse(se_num_sr) == 3)
        {
            sounds[1].PlayOneShot(se2);
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
            automode = 0;
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
            stillimage.sprite = still_May_hikaruSprite;
        }
        else if (int.Parse(stillsr) == 6)
        {
            stillimage.sprite = still_June_ouziSprite;
        }
        else if (int.Parse(stillsr) == 71)
        {
            stillimage.sprite = still_July_rukiaSprite; //7月
        }
        else if (int.Parse(stillsr) == 72)
        {
            stillimage.sprite = still_July_ouziSprite; //7月
        }
        else if (int.Parse(stillsr) == 73)
        {
            stillimage.sprite = still_July_hikaruSprite; //7月
        }
        else if (int.Parse(stillsr) == 81)
        {
            stillimage.sprite = still_August_ouziSprite; //8月おうじ
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
            startscene = 1;
            //スタート画面の切り替え
            PlayerPrefs.SetInt("START", 1);
            PlayerPrefs.Save();
        }
        if(int.Parse(monthsr) == 6)
        {
            monthimage.sprite = month_JuneSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 7)
        {
            monthimage.sprite = month_JulySprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 8)
        {
            monthimage.sprite = month_AugustSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 9)
        {
            monthimage.sprite = month_SeptemberSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 10)
        {
            monthimage.sprite = month_OctoberSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 11)
        {
            monthimage.sprite = month_NovemberSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 12)
        {
            monthimage.sprite = month_DecemberSprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 1)
        {
            monthimage.sprite = month_JanuarySprite;
            month.SetActive(true);
        }
        if (int.Parse(monthsr) == 2)
        {
            monthimage.sprite = month_FebruarySprite;
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
            backimage.sprite = still_May_hikaruSprite;
        }
        else if(int.Parse(backsr) == 8) {
            backimage.sprite = back_shoploadSprite;
        }
        else if(int.Parse(backsr) == 9)
        {
            backimage.sprite = back_fancyshopSprite;
        }
        else if (int.Parse(backsr) == 12)
        {
            backimage.sprite = back_heroineroom_nightSprite;
        }
        else if (int.Parse(backsr) == 11)
        {
            backimage.sprite = back_heroineroom_noonSprite;
        }
        else if (int.Parse(backsr) == 7)
        {
            backimage.sprite = back_groundSprite;
        }
        else if (int.Parse(backsr) == 10)
        {
            backimage.sprite = back_cafeSprite;
        }
        else if (int.Parse(backsr) == 13)
        {
            backimage.sprite = back_garageSprite;
        }
        else if (int.Parse(backsr) == 71)
        {
            backimage.sprite = still_July_rukiaSprite;
        }
        else if (int.Parse(backsr) == 72)
        {
            backimage.sprite = still_July_ouziSprite;
        }
        else if (int.Parse(backsr) == 73)
        {
            backimage.sprite = still_July_hikaruSprite;
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
        else if (int.Parse(centersr) == 9)
        { 
            centerCharacter.sprite = ouziojismileSprite;
        }
        else if (int.Parse(centersr) == 10)
        {
            centerCharacter.sprite = ouziojiangrySprite;
        }
        else if (int.Parse(centersr) == 11)
        {
            centerCharacter.sprite = ouziojitroubleSprite;
        }
        else if (int.Parse(centersr) == 12)
        {
            centerCharacter.sprite = rukiatroubleSprite;
        }
        else if (int.Parse(centersr) == 13)
        {
            centerCharacter.sprite = rukiaojismileSprite;
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
        else if (int.Parse(centersr) == 9)
        {
            rightCharacter.sprite = ouziojismileSprite;
        }
        else if (int.Parse(centersr) == 10)
        {
            rightCharacter.sprite = ouziojiangrySprite;
        }
        else if (int.Parse(centersr) == 11)
        {
            rightCharacter.sprite = ouziojitroubleSprite;
        }
        else if (int.Parse(centersr) == 12)
        {
            rightCharacter.sprite = rukiatroubleSprite;
        }
        else if (int.Parse(centersr) == 13)
        {
            rightCharacter.sprite = rukiaojismileSprite;
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
        else if (int.Parse(centersr) == 9)
        {
            leftCharacter.sprite = ouziojismileSprite;
        }
        else if (int.Parse(centersr) == 10)
        {
            leftCharacter.sprite = ouziojiangrySprite;
        }
        else if (int.Parse(centersr) == 11)
        {
            leftCharacter.sprite = ouziojitroubleSprite;
        }
        else if (int.Parse(centersr) == 12)
        {
            leftCharacter.sprite = rukiatroubleSprite;
        }
        else if (int.Parse(centersr) == 13)
        {
            leftCharacter.sprite = rukiaojismileSprite;
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
        else if (int.Parse(textcolorsr) == 5)
        {
            textboximage.sprite = clearSprite;
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
        index_read = index; //skip用
        while (_qdataList[index].storyText.Length > messageCount)
        {
                _story.text += _qdataList[index].storyText[messageCount];
                messageCount++;
                yield return new WaitForSeconds(novelSpeed);
        }
        if(_qdataList[index].storyText.Length == messageCount)
        {
            click = 1;
            if(novelspeedcount == 1)
            {
                novelSpeed = 0.3f;
            } else if(novelspeedcount == 2)
            {
                novelSpeed = 0.2f;
            } else if(novelspeedcount == 3)
            {
                novelSpeed = 0.15f;
            } else if(novelspeedcount == 4)
            {
                novelSpeed = 0.1f;
            } else
            {
                novelSpeed = 0.05f;
            }
            
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
            _logtext.text += "\n";
        }

        index_read = index;

//オートモード
        if (qNum > qstory && automode == 1)
        {
            sounds[2].Stop();

            if (!(selected == 1 && qstory == 91) && MenuPanel.activeSelf == false)
            {
                autoclick = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Menu();
        auto();
        //Debug.Log(MousePos.y);
    }
    public void onClicked_closebutton()
    {
        MenuPanel.SetActive(false);
    }
    public void onClick_Menu()
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
        if ( MenuPanel.activeSelf == false && menucount == 0)
        {
            savenum = 1;
            PlayerPrefs.SetInt("SAVE", savenum);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("NUMBER", qstory);
            PlayerPrefs.Save();
            MenuPanel.SetActive(true);
        } else if(MenuPanel.activeSelf == true && menucount > 30)
        {
            menucount = 30;
            MenuPanel.SetActive(false);
            
            //skip用
                cannotskipAlertPanel.SetActive(false);
            //
        }

    }

    public void onClick_Screenbutton()
    {
        if (automode != 1)
        {
            if (click == 0)
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
                if (!(selected == 2 && index_skip == 234))  //とめるところ
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                }
            }
        }
    }

    //選択肢ボタン選択時の動き
    public void onClick_SelectButton_3()
    {
        Debug.Log("3がおされている");
        selected = 3;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_1()
    {
        SkipselectPanelScript.clicked_skip = true;
        Debug.Log("1がおされている");
        selected = 1;
        //qstoryは調整する
        if(index_read == 67)
        {
            index_skip = 67;
        }
        else if(index_read == 110)
        {
            index_skip = 114;
        }
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_2()
    {
        SkipselectPanelScript.clicked_skip = true;
        Debug.Log("2がおされている");
        Debug.Log(index_read);
        selected = 2;
        //qstoryは調整する
        if (index_read == 67)
        {
            index_skip = 90;
        }
        else if (index_read == 110)
        {
            index_skip = 119;
        }
        else if(index_read == 138)
        {
            index_skip = 166;
        }
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }


    public void onClicked_settingbutton()
    {
        settingPanel.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out MousePos);
        if(novelspeedcount == 1)
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, 43);
        } else if(novelspeedcount == 2)
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-9, 43);
        } else if(novelspeedcount == 3)
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(91, 43);
        } else if(novelspeedcount == 4)
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(198, 43);
        } else
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, 43);
        }


        if (autospeedcount == 1)
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, 129);
        }
        else if (autospeedcount == 2)
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-9, 129);
        }
        else if (autospeedcount == 3)
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(91, 129);
        }
        else if (autospeedcount == 4)
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(198, 129);
        }
        else
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, 129);
        }


        if (sevolumecount == 1)
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, -52);
        }
        else if (sevolumecount == 2)
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-9, -52);
        }
        else if (sevolumecount == 3)
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(91, -52);
        }
        else if (sevolumecount == 4)
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(198, -52);
        }
        else
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, -52);
        }

        if (bgmvolumecount == 1)
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-133, -138);
        }
        else if (bgmvolumecount == 2)
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(-9, -138);
        }
        else if (bgmvolumecount == 3)
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(91, -138);
        }
        else if (bgmvolumecount == 4)
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(198, -138);
        }
        else
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(275, -138);
        }

    }

    public void speedslidedrag()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,Input.mousePosition, canvas.worldCamera, out MousePos);
        if ((MousePos.y <= 43 + 43 && MousePos.y >= 43 - 43) && MousePos.x > -149.36 && 291.96 > MousePos.x)
        {
            speedslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, 43);
        }
    }

    public void speedslidedrop()
    {
        speedbar_x = speedslide.transform.position.x;
        Debug.Log(speedbar_x);
        //Debug.Log(MousePos.y);
        if (speedbar_x > 169.6606f && 219.5f >= speedbar_x)
        {
            novelspeedcount = 1;
        }
        else if (speedbar_x > 219.5f && 270 >= speedbar_x)
        {
            novelspeedcount = 2;
        }
        else if (speedbar_x > 270 && 346.5f >= speedbar_x)
        {
            novelspeedcount = 3;
        }
        else if (speedbar_x > 346.5f && 421.5f > speedbar_x)
        {
            novelspeedcount = 4;
        }
        else if (speedbar_x > 421.5f && 465.3495f > speedbar_x)
        {
            novelspeedcount = 5;
        }

    }

    public void autoslidedrag()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out MousePos);
        if ((MousePos.y <= 129 + 43 && MousePos.y >= 129 - 43) && MousePos.x > -149.36 && 291.96 > MousePos.x)
        {
            autoslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, 129);
        }
    }
    public void autoslidedrop()
    {
        autoslidebar_x = autoslide.transform.position.x;
        Debug.Log(autoslidebar_x);
        //Debug.Log(MousePos.y);
        if (autoslidebar_x > 169.6606f && 219.5f >= autoslidebar_x)
        {
            autospeedcount = 1;
        }
        else if (autoslidebar_x > 219.5f && 270 >= autoslidebar_x)
        {
            autospeedcount = 2;
        }
        else if (autoslidebar_x > 270 && 346.5f >= autoslidebar_x)
        {
            autospeedcount = 3;
        }
        else if (autoslidebar_x > 346.5f && 421.5f > autoslidebar_x)
        {
            autospeedcount = 4;
        }
        else if (autoslidebar_x > 421.5f && 465.3495f > autoslidebar_x)
        {
            autospeedcount = 5;
        }

    }

    public void seslidedrag()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out MousePos);
        if ((MousePos.y <= -52 + 43 && MousePos.y >= -52 - 43) && MousePos.x > -149.36 && 291.96 > MousePos.x)
        {
            seslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, -52);
        }
    }

    public void seslidedrop()
    {
        float x_se;
        x_se = seslide.transform.position.x;
        //Debug.Log(x_se);
        //Debug.Log(MousePos.x);
        if (x_se > 169.6606f && 219.5f >= x_se)
        {
            sevolumecount = 1;
            sevolume = 0.1f;
        }
        else if (x_se > 219.5f && 270 >= x_se)
        {
            sevolumecount = 2;
            sevolume = 0.25f;
        }
        else if (x_se > 270 && 346.5f >= x_se)
        {
            sevolumecount = 3;
            sevolume = 0.5f;
        }
        else if (x_se > 346.5f && 421.5f > x_se)
        {
            sevolumecount = 4;
            sevolume = 0.7f;
        }
        else if (x_se > 421.5f && 465.3495f > x_se)
        {
            sevolumecount = 5;
            sevolume = 1;
        }

        sounds[1].volume = sevolume;
    }


    public void bgmslidedrag()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out MousePos);
        if ((MousePos.y <= -138 + 43 && MousePos.y >= -138 - 43) && MousePos.x > -149.36 && 291.96 > MousePos.x)
        {
            bgmslide.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, -138);
        }
    }

    public void bgmslidedrop()
    {
        float x_bgm;
        x_bgm = bgmslide.transform.position.x;
        //Debug.Log(x_se);
        Debug.Log(MousePos.x);
        if (x_bgm > 169.6606f && 219.5f >= x_bgm)
        {
            bgmvolumecount = 1;
            bgmvolume = 0.1f;
        }
        else if (x_bgm > 219.5f && 270 >= x_bgm)
        {
            bgmvolumecount = 2;
            bgmvolume = 0.25f;
        }
        else if (x_bgm > 270 && 346.5f >= x_bgm)
        {
            bgmvolumecount = 3;
            bgmvolume = 0.5f;
        }
        else if (x_bgm > 346.5f && 421.5f > x_bgm)
        {
            bgmvolumecount = 4;
            bgmvolume = 0.7f;
        }
        else if (x_bgm > 421.5f && 465.3495f > x_bgm)
        {
            bgmvolumecount = 5;
            bgmvolume = 1;
        }
        
        sounds[0].volume = bgmvolume;
    }

    public void OnClicked_SEMuteButton()
    {
        if(seMutecount == 0)
        {
            sounds[1].volume = 0;
            seMutecount = 1;
        } else if(seMutecount == 1)
        {
            sounds[1].volume = sevolume;
            seMutecount = 0;
        }
        
    }

    public void OnClicked_BGMMuteButton()
    {
        if (bgmMutecount == 0)
        {
            sounds[0].volume = 0;
            bgmMutecount = 1;
        }
        else if (bgmMutecount == 1)
        {
            sounds[0].volume = bgmvolume;
            bgmMutecount = 0;
        }
    }

    public void onClicked_settingreturnbutton()
    {
        settingPanel.SetActive(false);
        
        MenuPanel.SetActive(false);
    }

    public void onClicked_Autobutton()
    {
        MenuPanel.SetActive(false);
        if (automode == 0)
        {
            automode = 1;
            if (_qdataList[index_read].storyText.Length == messageCount)
            {
                StartCoroutine(Novel(qstory++));
            }
        }
        else
        {
            automode = 0;
            autoclick = false;
            time = 0;
        }
    }

    void auto()
    {
        if (autoclick == true)
        {
            time += Time.deltaTime;
            Debug.Log("time:" + time);
            if(autospeedcount == 1)
            {
                if (time > 1.5f)
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                    autoclick = false;
                    time = 0;
                }
            } else if(autospeedcount == 2)
            {
                if (time > 1)
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                    autoclick = false;
                    time = 0;
                }
            } else if(autospeedcount == 3)
            {
                if (time > 0.9f)
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                    autoclick = false;
                    time = 0;
                }
            } else if(autospeedcount == 4)
            {
                if (time > 0.7f)
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                    autoclick = false;
                    time = 0;
                }
            } else
            {
                if (time > 0.5f)
                {
                    StartCoroutine(Novel(qstory++));
                    click = 0;
                    autoclick = false;
                    time = 0;
                }
            }

        }
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
        //Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t" + selectbuttontext1 + "\t" + selectbuttontext2);
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
