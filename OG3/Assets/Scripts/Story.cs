using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
    public Sprite ouziSprite;
    public Sprite rukiaSprite;
    public Sprite hikaruSprite;
    public Sprite clearSprite;
    public Sprite statueSprite;
    public Sprite ouziojiSprite;
    public Sprite rukiaojiSprite;
    public Sprite hikaruojiSprite;
    public Sprite back_classroomSprite;
    public Sprite back_stairsSprite;
    public Sprite back_corridorSprite;
    public Sprite back_gardenSprite;
    public Sprite back_schoolSprite;
    public Sprite back_dispensarySprite;
    public Sprite blackSprite;
    public Sprite still_clearSprite;
    public Sprite still_AprilSprite;
    private string centersr;
    private string rightsr;
    private string leftsr;
    private string backsr;
    private string stillsr;

    GameObject _Screenbutton;//button
    public TextAsset storyText; //csvストーリーデータ
    private string _storyArray;
    private List<Qdata> _qdataList = new List<Qdata>();

    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数

    public float novelSpeed; //表示の速さ
    private int click = 0;


    // Start is called before the first frame update
    void Start()
    {
        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();
        _Screenbutton = GameObject.Find("Screenbutton");

        //csvファイルからテキストを読み込み
        StringReader sr = new StringReader(storyText.text);
        sr.ReadLine();
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            _qdataList.Add(new Qdata(line));
            qNum++;           
        }
        print("A");
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
        else if (int.Parse(leftsr) == 24)
        {
            leftCharacter.sprite = statueSprite;
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
    }

    public void onClick_Screenbutton()
    {
        if(click == 0)
        {
            novelSpeed = 0;
        }
        if (qNum > qstory && click == 1)
        {
            StartCoroutine(Novel(qstory++));
            click = 0;
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

    public Qdata(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 8)
        {
            number = int.Parse(spTxt[0]);
            storyText = spTxt[1];
            nameText = spTxt[2];
            centerimage = spTxt[3];
            rightimage = spTxt[4];
            leftimage = spTxt[5];
            backimage = spTxt[6];
            stillimage = spTxt[7];
            
        }

    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t");
    }

}