using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gallerysclipt : MonoBehaviour
{
    [SerializeField] GameObject galleryPanel;
    [SerializeField] GameObject minigamemenuPanel;
    [SerializeField] GameObject albumPanel;
    [SerializeField] GameObject scenarioPanel;

    // Start is called before the first frame update
    void Start()
    {
        galleryPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClicked_minigamemenubutton()
    {
        minigamemenuPanel.SetActive(true);
        galleryPanel.SetActive(false);

    }

    public void onClicked_albumbutton()
    {
        albumPanel.SetActive(true);
        galleryPanel.SetActive(false);

    }

    public void onClicked_scenariobutton()
    {
        scenarioPanel.SetActive(true);
        galleryPanel.SetActive(false);

    }

    public void onClicked_returnbutton()
    {
        scenarioPanel.SetActive(false);
        albumPanel.SetActive(false);
        minigamemenuPanel.SetActive(false);
        galleryPanel.SetActive(true);
    }

    public void onClicked_gachabutton()
    {
        SceneManager.LoadScene("gacha scene");
    }
}
