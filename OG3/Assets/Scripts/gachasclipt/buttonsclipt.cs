using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonsclipt : MonoBehaviour
{
    [SerializeField] GameObject resultpanel;
    [SerializeField] GameObject faImage;
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
            faImage.SetActive(false);
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


}
