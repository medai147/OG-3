using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipselectPanelScript : MonoBehaviour
{
    [SerializeField] GameObject SkipselectPanel;
    [SerializeField] GameObject Screenbutton;
    [SerializeField] GameObject LoadingPanel;
    public static bool clicked_skip;
    public static bool first;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_okButton()
    {
        SkipselectPanel.SetActive(false);
        Screenbutton.SetActive(true);
        clicked_skip = true;
        first = true;
        LoadingPanel.SetActive(true);
        if (Story.index_read < 49)
        {
            Story.index_skip = 49;
        } else if(Story.index_read > 49 && Story.index_read < 66)
        {
            Story.index_skip = 66;
        }
    }

    public void onClicked_backButton()
    {
        SkipselectPanel.SetActive(false);
        Screenbutton.SetActive(true);
    }
}
