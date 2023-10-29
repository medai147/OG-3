using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_newScript : MonoBehaviour
{
    public bool autoflag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCliked_autobutton()
    {
        if(!autoflag)
        {
            autoflag = true;
        } else
        {
            autoflag = false;
        }
    }
}
