using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gachasclipt : MonoBehaviour
{
    int value;
    int nowcoin;
    public Sprite bearsprite;
    public Sprite ringsprite;

    private float _feedTime = 0.1f; // 文字送り時間
    private float _t = 0f;
    private int _visibleLen = 0;
    private string _text = "";

    private float time;


    //public GameObject result;
    private Text _result;
    public Text cointext;
    [SerializeField] GameObject resultpanel;
    AudioSource fa;
    // Start is called before the first frame update
    void Start()
    {
        resultpanel.SetActive(false);
        fa = GetComponent<AudioSource>();
        nowcoin = PlayerPrefs.GetInt("NOWCOIN");
    }

    // Update is called once per frame
    void Update()
    {
        cointext.text = "所持金:" + nowcoin;
        PlayerPrefs.SetInt("NOWCOIN", nowcoin);
        PlayerPrefs.Save();
        MoveText();
    }

    public void onClicked_gacha()
    {
        if (nowcoin >= 50)
        {
            nowcoin -= 50;
            PlayerPrefs.SetInt("NOWCOIN", nowcoin);
            PlayerPrefs.Save();
            fa.Play();
            resultpanel.SetActive(true);
            value = Random.Range(0, 2);
            //Image resultimage = (Image)GameObject.Find("resultImage").GetComponent<Image>();
            _result = GameObject.Find("resulttext").GetComponent<Text>();
            if (value == 0)
            {
                    //resultimage.sprite = bearsprite;
                    SetText("僕の分のチョコはないの？");
                    
            }
            else if (value == 1)
            {
                //resultimage.sprite = ringsprite;
                SetText("俺、甘いものは苦手なんだ");
            }
        }
    }

    public void SetText(string text)
    {
        _text = text;
        _visibleLen = 0;
        _t = 0;
        _result.text = "";
    }

    public void MoveText()
    {
        time += Time.deltaTime;
        if (time > 4.5f)
        {
            if (_visibleLen < _text.Length)
            {
                _t += Time.deltaTime;
                if (_t >= _feedTime)
                {
                    _t -= _feedTime;
                    _visibleLen++;
                    _result.text = _text.Substring(0, _visibleLen); // 1文字ずつ増やす
                }
            } else
            {
                time = 0;
            }
        }
    }
}
