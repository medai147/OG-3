using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menusclipt : MonoBehaviour
{
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
}
