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
    public Image ouzi;//Image
    GameObject _Screenbutton;//button
    GameObject _Menubutton;
    public TextAsset storyText; //csvストーリーデータ
    private string _storyArray;
    private List<Qdata> _qdataList = new List<Qdata>();

    public int qstory = 0; //storyの番号
    public int qNum = 0; //story数

    // Start is called before the first frame update
    void Start()
    {
        _story = GameObject.Find("MainText").GetComponent<Text>();
        _name = GameObject.Find("NameText").GetComponent<Text>();
        _Screenbutton = GameObject.Find("Screenbutton");
        _Menubutton = GameObject.Find("Menubutton");

        //csvファイルからテキストを読み込み
        StringReader sr = new StringReader(storyText.text);
        sr.ReadLine();
        while (sr.Peek() > -1)
        {
            string line = sr.ReadLine();
            _qdataList.Add(new Qdata(line));
            qNum++;           
        }
        setStory(qstory++);
        print("A");
          //最初のストーリーをセット
        //確認のためにConsoleに出力
        foreach (Qdata q in _qdataList)
        {
            q.WriteDebugLog();
        }


    }

    //質問番号を渡すと、その番号の質問をセットするメソッド
    private void setStory(int index)
    {
        _story.text = _qdataList[index].storyText;  //質問文テキストに質問文をセット
        _name.text = _qdataList[index].nameText;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onClick_Screenbutton()
    {
        if (qNum > qstory)
        {
            setStory(qstory++);
        }
    }

    public void onClick_Menubutton()
    {
        print("click");
    }
}

//質問を管理するクラス
public class Qdata
{
    int number;
    public string storyText;
    public string imageText;
    public string nameText;

    public Qdata(string txt)
    {
        string[] spTxt = txt.Split(',');
        if (spTxt.Length == 4)
        {
            number = int.Parse(spTxt[0]);
            storyText = spTxt[1];
            imageText = spTxt[2];
            nameText = spTxt[3];
        }

    }

    public void WriteDebugLog()
    {
        Debug.Log(number + "\t" + storyText + "\t" + imageText + "\t" + nameText + "\t");
    }

}