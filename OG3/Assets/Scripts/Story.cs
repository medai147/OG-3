using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    private Text _story; //ストーリーテキスト
    private Text _name;
    private AudioSource Soundbgm; //bgm
    public GameObject charactercenter;
    public GameObject characterright;
    public GameObject characterleft;
    public GameObject background;
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
    public Sprite back_classroomSprite;
    public Sprite back_stairsSprite;
    public Sprite back_corridorSprite;
    public Sprite back_gardenSprite;
    public Sprite back_schoolSprite;
    public Sprite back_dispensarySprite;
    public Sprite blackSprite;
    public Sprite still_clearSprite;
    public Sprite still_AprilSprite;
    public Sprite still_May_ouzi_Sprite;
    public Sprite still_May_hikaru_Sprite;
    public Sprite month_MaySprite;
    public Sprite month_clearSprite;
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

    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数
    public int savenum = 0;
    public float novelSpeed; //表示の速さ
    private int click = 0;

    [SerializeField] GameObject ScreenButton;
    [SerializeField] GameObject SelectButtonPanel;

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

        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();
        _Screenbutton = GameObject.Find("Screenbutton");

        PlayerPrefs.SetInt("SAVE", 0);
        PlayerPrefs.Save();

        qstory = PlayerPrefs.GetInt("NUMBERLOAD");

        //csvファイルからテキストを読み込み
        StringReader sr = new StringReader(storyText.text);
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
            q.WriteDebugLog();
        }
        StartCoroutine(Novel(qstory++));


    }
    private IEnumerator Novel(int index)
    {
        int messageCount = 0; //表示中の文字数
        _story.text = "";
        _name.text = _qdataList[index].nameText;

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
        Debug.Log(index);
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
        if (int.Parse(selectdisp_sr) == 1)
        {
            novelSpeed = 0;
            ScreenButton.SetActive(false);
            SelectButtonPanel.SetActive(true);
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
            stillimage.sprite = still_May_ouzi_Sprite;
        }
        else if (int.Parse(stillsr) == 6)
        {
            stillimage.sprite = still_May_hikaru_Sprite;
        }

        //月のはじめの画像
        monthsr = _qdataList[index].monthimage;
        Image monthimage = (Image)month.GetComponent<Image>();
        if (int.Parse(monthsr) == 0)
        {
            monthimage.sprite = month_clearSprite;
        }
        if (int.Parse(monthsr) == 5)
        {
            monthimage.sprite = month_MaySprite;
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
        else if(int.Parse(backsr) == 30)
        {
            backimage.sprite = still_AprilSprite;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        Keysave();
    }

    private void Keysave()
    {
        if (Input.GetKey(KeyCode.S))
        {
            savenum = 1;
            PlayerPrefs.SetInt("SAVE", savenum);
            PlayerPrefs.Save();
            PlayerPrefs.SetInt("NUMBER", qstory);
            PlayerPrefs.Save();
            SceneManager.LoadScene("save scene");
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
            StartCoroutine(Novel(qstory++));
            click = 0;
        }
    }

    public void onClick_SelectButton_3()
    {
        selected = 1;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_1()
    {
        selected = 1;
        SelectButtonPanel.SetActive(false);
        ScreenButton.SetActive(true);
        onClick_Screenbutton();
    }

    public void onClick_SelectButton_2()
    {
        selected = 1;
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

    public Qdata(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 15)
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
        }
    }

    public void WriteDebugLog()
    {
        //Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t");
    }

}

