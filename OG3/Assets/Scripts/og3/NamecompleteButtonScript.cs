using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NamecompleteButtonScript : MonoBehaviour
{
    public InputField _inputName;
    public String heroineName;
    [SerializeField] GameObject InputNamePanel;
    [SerializeField] GameObject LoadingPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClicked_NamecompleteButton()
    {
        LoadingPanel.SetActive(true);
        heroineName = _inputName.text;
        //Debug.Log(heroineName);

        GameManager.instance.gameStateManager.heroineName = heroineName;
        SceneManager.LoadScene("Mainscene_new2");
    }
}
