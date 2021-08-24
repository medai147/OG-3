using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class minigamesclipt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void minigame_onClicked()
    {
        Invoke("jumptoMinigame", 0.52f);
    }

    public void jumptoMinigame()
    {
        SceneManager.LoadScene("minigamemenuscene");
    }

}
