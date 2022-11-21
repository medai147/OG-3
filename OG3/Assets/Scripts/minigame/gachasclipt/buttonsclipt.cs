using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonsclipt : MonoBehaviour
{
    [SerializeField] GameObject resultpanel;

    public int time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resultpanel.activeSelf == true)
        {
            time++;
        }
        if(time > 350)
        {
        }
    }
    public void onClicked_resultreturn()
    {
        if(time > 400)
        {
            resultpanel.SetActive(false);
            time = 0;
        }
    }
    public void onClicked_returnmenubutton()
    {
        SceneManager.LoadScene("start scene");
    }


}
