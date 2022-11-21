using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloselogButtonScript : MonoBehaviour
{
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject Screenbutton;
    [SerializeField] GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCLicked_CloselogButton()
    {
        logPanel.SetActive(false);
        Screenbutton.SetActive(true);
        menuPanel.SetActive(true);
    }
}
