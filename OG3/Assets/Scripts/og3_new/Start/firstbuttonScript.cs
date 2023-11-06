using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstbuttonScript : MonoBehaviour
{
    [SerializeField] GameObject inputnamePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_firststartbutton()
    {
        inputnamePanel.SetActive(true);
    }
}
