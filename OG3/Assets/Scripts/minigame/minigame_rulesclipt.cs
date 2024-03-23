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
    }

    public void onClicked_returnbutton()
    {
        SceneManager.LoadScene("minigamemenuscene");
    }

    public void onClicked_titlebackbutton()
    {
        SceneManager.LoadScene("startscene_new");
    }
}
