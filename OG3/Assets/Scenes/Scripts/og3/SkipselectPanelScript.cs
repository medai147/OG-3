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
            Story.index_skip = 49; //5月に飛ぶ
        }
        else if(Story.index_read > 49 && Story.index_read < 66)
        {
            Story.index_skip = 66; //5月ひかるの選択肢に飛ぶ
        }
        else if (Story.index_read > 66 && Story.index_read < 94)
        {
            Story.index_skip = 95; //5月おうじの先頭に飛ぶ
        }
        else if(Story.index_read > 94 && Story.index_read < 108)
        {
            Story.index_skip = 114; //5月おうじの選択肢に飛ぶ
        }
        else if (Story.index_read > 109 && Story.index_read < 124)
        {
            Story.index_skip = 131; //6月に飛ぶ
        }
        else if (Story.index_read > 124 && Story.index_read < 137)
        {
            Story.index_skip = 145; //6月おうじの選択肢に飛ぶ
        }
        else if (Story.index_read > 137 && Story.index_read < 171)
        {
            Story.index_skip = 181; //7月に飛ぶ
        }
        else if (Story.index_read > 171 && Story.index_read < 188)
        {
            Story.index_skip = 199; //7月おうじに飛ぶ
        }
        else if (Story.index_read > 188 && Story.index_read < 210)
        {
            Story.index_skip = 222; //7月ひかるに飛ぶ
        }
    }

    public void onClicked_backButton()
    {
        SkipselectPanel.SetActive(false);
        Screenbutton.SetActive(true);
    }
}
