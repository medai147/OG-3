using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_newScript : MonoBehaviour
{
    [SerializeField] GameObject settingPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickd_returnbutton()
    {
        settingPanel.SetActive(false);
    }
}
