using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class logstorytextScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset logstoryText; //csvlogstorytextデータ
    private string _logstoryArray;
    private List<Qdata> _logdataList = new List<Qdata>();
    int logNum = 0; //story数
    void Start()
    {
        //csvファイルからテキストを読み込み
        _logstoryArray = logstoryText.text.Replace(" ", "\u00A0");
        //_logstoryArray = logstoryText.text.Replace("?", "\u0030");
        StringReader logr = new StringReader(_logstoryArray);
        logr.ReadLine();
        while(logr.Peek() > -1)
        {
            string line = logr.ReadLine();
            _logdataList.Add(new Qdata(line));
            logNum++;
        }
        foreach (Qdata q in _logdataList)
        {
            q.WriteDebugLog();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        public string check;
        int nokori1;
        int nokori2;
        int nokori3;
        int nokori4;
        int nokori5;
        int nokori6;

        public Logdata(string txt)
        {
            string[] spTxt = txt.Split(',');
            if (spTxt.Length == 16) {
                textnumber = int.Parse(spTxt[0]);
                logstorytext1 = spTxt[1];
                logstorytext2 = spTxt[2];
                logstorytext3 = spTxt[3];
                logstorytext4 = spTxt[4];
                logstorytext5 = spTxt[5];
                logstorytext6 = spTxt[6];
                lengthcount = int.Parse(spTxt[7]);
                mainstory = spTxt[8];
                check = spTxt[9];
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
            Debug.Log(logstorytext1 + "\t" + logstorytext2 + "\t" + logstorytext3 + "\t" + logstorytext4 + "\t" + logstorytext5 + "\t" + logstorytext6);
        }
    }

}
