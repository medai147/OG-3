using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputlogtext_newScript : MonoBehaviour
{
    GameObject logtextobj;
    Text logtext;

    bool textactive = false;
    // Start is called before the first frame update
    void Start()
    {
        logtextobj = this.gameObject;
        logtext = logtextobj.GetComponent<Text>();

        //始めてログを開いたときしか動かないから修正する必要あり
        foreach (string log in GameManager.instance.logtext)
        {
            logtext.text += log;
        }
    }

    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    { 
    }
}
