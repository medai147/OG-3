using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class minigamesclipt : MonoBehaviour
{
    public int startchange;
    public GameObject startbackimage;
    public Sprite ojistartimageSprite;

    [SerializeField] AudioSource audiosource;
    public AudioClip button;
    // Start is called before the first frame update
    void Start()
    {
        /*
        startchange = PlayerPrefs.GetInt("START");
        GameObject startimage = startbackimage.GetComponent<Image>();
        if(startchange == 1)
        {
            startimage.sprite = ojistartimageSprite;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("スタート：" + startchange);
    }
    public void minigame_onClicked()
    {
        // if (startchange == 1)
        // {
        //   Invoke("jumptoMinigame", 0.52f);
        //}

        SceneManager.LoadScene("minigamemenuscene");
    }



    public void onClicked_minigamebutton()
    {
        audiosource.PlayOneShot(button);
        Invoke("loadbutton", 1.0f);
    }

    void loadbutton()
    {
        SceneManager.LoadScene("minigamemenuscene");
    }

}
