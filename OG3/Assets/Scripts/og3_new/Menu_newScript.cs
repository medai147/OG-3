using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_newScript : MonoBehaviour
{
    [SerializeField]GameObject menuPanel;
    [SerializeField] GameObject settingPanel;

    bool deleteflag = false;

    GameObject[] deletes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public void onClicked_menubutton()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void onClicked_closebutton()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void onClicked_deletebutton()
    {
        deleteflag = true;
        deletes = GameObject.FindGameObjectsWithTag("delete");
        for(int i = 0; i < deletes.Length;i++)
        {
            deletes[i].SetActive(false);
        }
    }

    public void onClicked_screenbutton()
    {
        if (deleteflag)
        {
            deleteflag = false;
            for (int i = 0; i < deletes.Length; i++)
            {
                deletes[i].SetActive(true);
            }
        }
    }

    public void onClickd_settingbutton()
    {
        settingPanel.SetActive(true);
    }
}
