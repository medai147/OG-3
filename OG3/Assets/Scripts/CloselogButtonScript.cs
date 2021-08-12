using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloselogButtonScript : MonoBehaviour
{
    [SerializeField] GameObject logPanel;

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
    }
}
