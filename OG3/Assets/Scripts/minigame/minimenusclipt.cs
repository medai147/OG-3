using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minimenusclipt : MonoBehaviour
{
    int nowcoin;
    public Text cointext;
    // Start is called before the first frame update
    void Start()
    {
        nowcoin = PlayerPrefs.GetInt("NOWCOIN");
        cointext.text = "所持金:" + nowcoin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_icebutton()
    {
        SceneManager.LoadScene("icegame scene");
    }
    public void onClicked_gachabutton()
    {
        SceneManager.LoadScene("gacha scene");
    }
}
