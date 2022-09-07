using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minimenusclipt : MonoBehaviour
{
    //int nowcoin;
    public Text cointext;
    //static int count;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("カウント" + count);
        //count = PlayerPrefs.GetInt("COUNTERCOUNT");
        //nowcoin = PlayerPrefs.GetInt("NOWCOIN");
        //if (count == 0)
        //{
            //nowcoin = 0;
        //}
        //cointext.text = "所持金:" + nowcoin;

    }

    // Update is called once per frame
    void Update()
    {

        //PlayerPrefs.SetInt("NOWCOIN", nowcoin);
        //PlayerPrefs.Save();
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
