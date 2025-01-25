using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadbuttonScript : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    public AudioClip button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_loadbutton()
    {
        audiosource.PlayOneShot(button);
        Invoke("loadbutton", 1.0f);
    }

    void loadbutton()
    {
        SceneManager.LoadScene("loadscene");
        GameManager.instance.previousSceneName = "startscene_new2";
    }


}
