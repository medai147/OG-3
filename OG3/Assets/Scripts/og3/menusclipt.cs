using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusclipt : MonoBehaviour
{
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject Screenbutton;
    [SerializeField] GameObject TextImage;
    [SerializeField] GameObject MainTextPanel;
    [SerializeField] GameObject NameTextPanel;
    [SerializeField] GameObject monthtext;
    [SerializeField] GameObject SelectButtonPanel;
    [SerializeField] GameObject deleteResetButton;
    private int selectState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_Save()
    {
        SceneManager.LoadScene("save scene");
    }

    public void onClicked_Start()
    {
        SceneManager.LoadScene("start scene");
    }

    public void onClicked_log()
    {
        logPanel.SetActive(true);
        MenuPanel.SetActive(false);
        Screenbutton.SetActive(false);
    }

    public void onClicked_delete()
    {
        TextImage.SetActive(false);
        MainTextPanel.SetActive(false);
        NameTextPanel.SetActive(false);
        monthtext.SetActive(false);
        Screenbutton.SetActive(false);
        if(SelectButtonPanel.activeSelf == true) {
            //Debug.Log("選択肢あり");
            SelectButtonPanel.SetActive(false);
        }
        MenuPanel.SetActive(false);
        deleteResetButton.SetActive(true);
    }

    public void onClicked_deleteResetButton()
    {

        TextImage.SetActive(true);
        MainTextPanel.SetActive(true);
        NameTextPanel.SetActive(true);
        monthtext.SetActive(true);
        Screenbutton.SetActive(true);
        selectState = Story.selectpanelState;
        if (selectState == 1)
        {
            SelectButtonPanel.SetActive(true);
            Screenbutton.SetActive(false);
        }
        deleteResetButton.SetActive(false);
    }
}
