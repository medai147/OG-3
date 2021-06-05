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
    public Sprite ouziSprite;
    public Sprite rukiaSprite;
    public Sprite clearSprite;
    private string centersr;
    private string rightsr;
    private string leftsr;

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

        //センター画像
        centersr = _qdataList[index].centerimage;
        Image centerCharacter = (Image)charactercenter.GetComponent<Image>();
        if (int.Parse(centersr) == 1)
        {
            centerCharacter.sprite = ouziSprite;
        }else if(int.Parse(centersr) == 2)
        {
            centerCharacter.sprite = rukiaSprite;
        } else if(int.Parse(centersr) == 0)
        {
            centerCharacter.sprite = clearSprite;
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

    public Qdata(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 6)
        {
            number = int.Parse(spTxt[0]);
            storyText = spTxt[1];
            nameText = spTxt[2];
            centerimage = spTxt[3];
            rightimage = spTxt[4];
            leftimage = spTxt[5];
            
        }

    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + centerimage + "\t" + nameText + "\t");
    }

}