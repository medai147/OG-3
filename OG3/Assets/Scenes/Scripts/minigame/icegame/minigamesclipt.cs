using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minigamesclipt : MonoBehaviour
{
    public int startchange;
    public GameObject startbackimage;
    public Sprite ojistartimageSprite;
    // Start is called before the first frame update
    void Start()
    {
        startchange = PlayerPrefs.GetInt("START");
        Image startimage = (Image)startbackimage.GetComponent<Image>();
        if(startchange == 1)
        {
            startimage.sprite = ojistartimageSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("スタート：" + startchange);
    }
    public void minigame_onClicked()
    {
        if (startchange == 1)
        {
            Invoke("jumptoMinigame", 0.52f);
        }
    }

    public void jumptoMinigame()
    {

            SceneManager.LoadScene("minigamemenuscene");
    }

}
