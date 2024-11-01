using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Story_new : MonoBehaviour
{

    //�d�l����
    /*
     * �X�L�b�v�͎��̌��܂��͑I����
     */

    public static Story_new instance;
    
    
    public bool textnextflag = false;
    private bool automodeflag = false;

    bool textread = false; //�����Đ���
    public bool animationfinishedflag = true;

    private Text _story; //�X�g�[���[�e�L�X�g
    private Text _name;

    private Text _move; //�ړ��A�j���[�V�������̃e�L�X�g


    public TextAsset storyText; //csv�X�g�[���[�f�[�^
    private string _storyArray;
    private List<Qdata_new> _qdataList = new List<Qdata_new>();

    AudioClip oldbgmClip = null;
    AudioClip bgmClip = null;
    AudioClip seClip = null;


    public int qstory = 0; //story�̔ԍ�
    public int qNum = 0; //story��
    int messageCount = 0; //�\������Ă��镶���̐�
    float novelspeed = 0.1f; //�����̕\�����x

    public int nameinput = 0;

    public float[]textspeed = {0.3f,0.1f,0.05f};
    public float[] autospeed = {3f,2f,1f};

    String[] selectskipnum;//�I�������������Ƃ��̑J�ڐ�̔ԍ�

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


    [SerializeField] Auto_newScript autoscript;
    [SerializeField] Setting_newScript settingscript;



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
        //�I�[�f�B�I�\�[�X
        sounds = GetComponents<AudioSource>();
        sounds[0].Play();

        //�e�L�X�g
        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();


        //csv�t�@�C������e�L�X�g��ǂݍ���
        _storyArray = storyText.text.Replace(" ", "\u00A0");
        _storyArray = storyText.text.Replace("@", GameManager.instance.heroinename);
        StringReader sr = new StringReader(_storyArray);
        sr.ReadLine();
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            _qdataList.Add(new Qdata_new(line));
            qNum++;
        }
        //�ŏ��̃X�g�[���[���Z�b�g
        //�m�F�̂��߂�Console�ɏo��
        foreach (Qdata_new q in _qdataList)
        {
            //q.WriteDebugLog();
        }

        if(GameManager.instance.storynum != 0)
        {
            qstory = GameManager.instance.storynum - 1;
        } else
        {
            qstory = GameManager.instance.storynum;
        }
        
        //�ŏ��̃X�^�[�g������ύX����
        StartCoroutine(Novel(qstory));
    }

    // Update is called once per frame
    void Update()
    {
        if(qstory == 50)
        {
            SceneManager.LoadScene("FinishScene");
        }
        //Debug.Log("auto:" + settingscript.selectautobutton + " text:" + settingscript.selecttextbutton + "  bgm:" + settingscript.selectbgmbutton + " se:" + settingscript.selectsebutton);
        if (qstory == 93)
        {
            qstory = 98;
        }
        if (qstory == 122 || qstory == 130)
        {
            qstory = 138;
        }
        if (qstory == 183)
        {
            qstory = 187;
        }
        if(qstory == 253)
        {
            qstory = 0;
        }
        //�R���[�`����i�߂�
        if ((textnextflag && animationfinishedflag))
        {
            //�e�L�X�g��ǂޑ��x
            novelspeed = textspeed[settingscript.selecttextbutton - 1];
            StartCoroutine(Novel(qstory));
        } else
        {
            //�ݒ��ʂŕύX�����l�������ɔ��f�����悤��
            if(novelspeed != 0)
            {
                novelspeed = textspeed[settingscript.selecttextbutton - 1];
            }
        }



        //�I�[�g��
        if (autoscript.autoflag && !textread && !automode_textsendflag)
        {
            automode_textsendflag = true;
            Invoke("automode_textsend", autospeed[settingscript.selectautobutton - 1]);
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


        //���O��month�������Ă���ꏊ��ʉ߂�����e�L�X�g�ύX
        if (_qdataList[index].nameText.Contains("month"))
        {
            //����̌��\��
            GameObject.Find("monthtext").GetComponent<Text>().text = _qdataList[index].nameText.Replace("month", "");
        }

        //���O
        String textcolorsr = _qdataList[index].textcolor;
        if (textcolorsr.Equals("text_own"))
        {
           
            _name.text = GameManager.instance.heroinename;
        }
        else if (textcolorsr.Equals("text_monologue"))
        {
            _name.text = "";
        }
        else
        {
            _name.text = _qdataList[index].nameText;
        }

        //�I��(��)
        String[] selecttext;
        
        selecttext = _qdataList[index].selectbuttontext1.Split(' ');
        selectskipnum = _qdataList[index].selectbuttontext2.Split(' ');
        int selectcount = 0;
        foreach (string select in selecttext)
        {
            if(select != "0")
            {
                selectcount++;
            }
        }
        SelectButtonPanel.SetActive(false);
        SelectButton_3.SetActive(false);
        if (selectcount == 2)
        {
            SelectButtonPanel.SetActive(true);

            autoscript.autoflag = false;
            Selectbutton_1.transform.GetChild(0).GetComponent<Text>().text = selecttext[0];
            Selectbutton_2.transform.GetChild(0).GetComponent<Text>().text = selecttext[1];
        }
        else if (selectcount == 3)
        {
            SelectButtonPanel.SetActive(true);
            SelectButton_3.SetActive(true);

            autoscript.autoflag = false;
            Selectbutton_1.transform.GetChild(0).GetComponent<Text>().text = selecttext[0];
            Selectbutton_2.transform.GetChild(0).GetComponent<Text>().text = selecttext[1];
            SelectButton_3.transform.GetChild(0).GetComponent<Text>().text = selecttext[2];
        }



        //�{���Đ�
        _story.text = "";
        messageCount = 0;
        while (_qdataList[index].storyText.Length > messageCount)
        {
            textread = true;
            _story.text += _qdataList[index].storyText[messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelspeed);
        }


        if(_name.text == "")
        {
            GameManager.instance.logtext.Add("\n" + _qdataList[index].storyText);
        } else
        {
            GameManager.instance.logtext.Add("\n" + _name.text + ":" + _qdataList[index].storyText);
        }
        
        textread = false;

        //�X�g�[���[�ԍ������ɐi�߂�
        qstory++;
        GameManager.instance.storynum = qstory;
    }

    public bool automode_textsendflag = false;
    private void automode_textsend()
    {
        if(automode_textsendflag)
        {
            //�I�[�g���[�h���̎��̃e�L�X�g�ɍs���t���O
            textnextflag = true;
            automode_textsendflag = false;
        }

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

    public void onClicked_selectbutton_1()
    {
        qstory = int.Parse(selectskipnum[0]);
        textnextflag = true;
    }

    public void onClicked_selectbutton_2()
    {
        qstory = int.Parse(selectskipnum[1]);
        textnextflag = true;
    }

    public void onClicked_selectbutton_3()
    {
        qstory = int.Parse(selectskipnum[2]);
        textnextflag = true;
    }

    public void onClicked_automodebutton()
    {
        //�I�[�g���[�h�X�^�[�g
        automodeflag = true;
    }

    private void stilldisplay()
    {
        //�ꖇ�G
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
        //���̂͂��߂̉摜
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
        //�w�i
        String backsr = _qdataList[qstory].backimage;
        Image backimage = (Image)GameObject.Find("backgroundImage").GetComponent<Image>();
        backimage.sprite = Resources.Load<Sprite>("Sprites/back/" + backsr);
    }

    private void characterdisplay()
    {
        //�Z���^�[�摜
        String centersr = _qdataList[qstory].centerimage;
        Image centerCharacter = (Image)GameObject.Find("CenterCharacterImages").GetComponent<Image>();
        centerCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + centersr);


        //���C�g�摜
        String rightsr = _qdataList[qstory].rightimage;
        Image rightCharacter = (Image)GameObject.Find("RightCharacterImages").GetComponent<Image>();
        rightCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + rightsr);


        //���t�g�摜
        String leftsr = _qdataList[qstory].leftimage;
        Image leftCharacter = (Image)GameObject.Find("LeftCharacterImages").GetComponent<Image>();
        leftCharacter.sprite = Resources.Load<Sprite>("Sprites/human/" + leftsr);
    }

    private void charactercolor() {
        //�摜�̐F
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
        //�e�L�X�g�{�b�N�X�̐F
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
            Debug.Log("�A�j���[�V�������o��");
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
        sounds[0].volume = (float)settingscript.selectbgmbutton / 3;
        if(!bgm_sr.Equals("0"))
        {
            bgmClip = Resources.Load<AudioClip>("AudioClips/" + bgm_sr);
            
            if (oldbgmClip != bgmClip)
            {
                Debug.Log(oldbgmClip + " " + bgmClip);
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
        sounds[1].volume = (float)settingscript.selectsebutton / 3;
        if (!se_sr.Equals("0"))
        {
            seClip = Resources.Load<AudioClip>("AudioClips/" + se_sr);

            sounds[1].PlayOneShot(seClip);
        }
    }

}

//������Ǘ�����N���X
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
            selectdisp = spTxt[11];//�I������\�����邩�ǂ���
            selectbutton_num = spTxt[12];//�I�����̐��H
            monthimage = spTxt[13];
            selectbuttontext3 = spTxt[14];//�I�����ɕ\�����镶����
            selectbuttontext1 = spTxt[15];//�I�����ɕ\�����镶����
            selectbuttontext2 = spTxt[16];//�I�����ɕ\�����镶����
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