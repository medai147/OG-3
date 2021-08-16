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
        Debug.Log(nowcoin);
    }

    // Update is called once per frame
    void Update()
    {
        cointext.text = "所持金:" + nowcoin;
        PlayerPrefs.SetInt("NEWCOIN", nowcoin);
        PlayerPrefs.Save();
    }

    public void onClicked_gacha()
    {
        if (nowcoin > 50)
        {
            nowcoin -= 50;
            fa.Play();
            resultpanel.SetActive(true);
            value = Random.Range(0, 2);
            Image resultimage = (Image)GameObject.Find("resultImage").GetComponent<Image>();
            _result = GameObject.Find("resulttext").GetComponent<Text>();
            if (value == 0)
            {
                resultimage.sprite = bearsprite;
                _result.text = "ぬいぐるみゲット！";
            }
            else if (value == 1)
            {
                resultimage.sprite = ringsprite;
                _result.text = "指輪ゲット！";
            }
        }
    }
}
