using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_newScript : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;

    [SerializeField] GameObject[] autobuttons = new GameObject[3];

    [SerializeField] GameObject[] textbuttons = new GameObject[3];

    [SerializeField] GameObject[] sebuttons = new GameObject[3];

    [SerializeField] GameObject[] bgmbuttons = new GameObject[3];

    public int selectautobutton = 2;
    public int selecttextbutton = 2;
    public int selectsebutton = 3;
    public int selectbgmbutton = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickd_returnbutton()
    {
        settingPanel.SetActive(false);
    }

    public void onClicked_autobuttons(int number)
    {
        for (int i = 0; i < autobuttons.Length; i++)
        {
            autobuttons[i].GetComponent<Image>().enabled = false;
        }
        selectautobutton = number;
        autobuttons[number - 1].GetComponent<Image>().enabled = true;
    }

    public void onClicked_textbuttons(int number)
    {
        for (int i = 0; i < textbuttons.Length; i++)
        {
            textbuttons[i].GetComponent<Image>().enabled = false;
        }
        selecttextbutton = number;
        textbuttons[number - 1].GetComponent<Image>().enabled = true;
    }

    public void onClicked_sebuttons(int number)
    {
        for (int i = 0; i < sebuttons.Length; i++)
        {
            sebuttons[i].GetComponent<Image>().enabled = false;
        }
        selectsebutton = number;
        sebuttons[number - 1].GetComponent<Image>().enabled = true;
    }

    public void onClicked_bgmbuttons(int number)
    {
        for (int i = 0; i < bgmbuttons.Length; i++)
        {
            bgmbuttons[i].GetComponent<Image>().enabled = false;
        }
        selectbgmbutton = number;
        bgmbuttons[number - 1].GetComponent<Image>().enabled = true;
    }


}
