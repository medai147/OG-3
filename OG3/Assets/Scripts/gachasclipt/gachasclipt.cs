using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gachasclipt : MonoBehaviour
{
    int value;
    public Sprite bearsprite;
    public Sprite ringsprite;
    //public GameObject result;
    private Text _result;
    [SerializeField] GameObject resultpanel;
    AudioSource fa;
    // Start is called before the first frame update
    void Start()
    {
        resultpanel.SetActive(false);
        fa = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_gacha()
    {
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
