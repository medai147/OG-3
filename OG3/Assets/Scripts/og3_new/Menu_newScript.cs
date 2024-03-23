using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_newScript : MonoBehaviour
{
    [SerializeField]GameObject menuPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject logPanel;

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
        Invoke("menuactive", 0.1f);
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

    public void onClicked_logbutton()
    {
        logPanel.SetActive(true);
    }

    public void onClicked_savebutton()
    {
        GameManager.instance.beforescene = 1;
        SceneManager.LoadScene("savescene_new");
    }

    public void menuactive()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void onClicked_titleback()
    {
        GameManager.instance.beforescene = 1;
        Time.timeScale = 1;
        SceneManager.LoadScene("startscene_new");

    }
}
