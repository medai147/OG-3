using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minigame_rulesclipt : MonoBehaviour
{

    [SerializeField] GameObject minigamerulePanel1;
    [SerializeField] GameObject minigamerule;
    public Sprite minigameruleimage1;
    public Sprite minigameruleimage2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_minigamerule1()
    {
        minigamerulePanel1.SetActive(true);
    }

    public void onClicked_minigameruleclose()
    {
        minigamerulePanel1.SetActive(false);
        Image ruleimage = (Image)minigamerule.GetComponent<Image>();
        ruleimage.sprite = minigameruleimage1;
    }

    public void onClicked_returnbutton()
    {
        SceneManager.LoadScene("start scene");
    }

    public void onClicked_rulenextpagebutton()
    {
        Image ruleimage = (Image)minigamerule.GetComponent<Image>();
        ruleimage.sprite = minigameruleimage2;
    }
}
