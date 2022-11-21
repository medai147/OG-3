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
            Story.index_read = 49;
        }
        else if(Story.index_read > 49 && Story.index_read < 66)
        {
            Story.index_skip = 66; //5月ひかるの選択肢に飛ぶ
            Story.index_read = 66;
        }
        else if (Story.index_read > 66 && Story.index_read < 102)
        {
            Story.index_skip = 102; //5月おうじの先頭に飛ぶ
            Story.index_read = 102;
        }
        else if(Story.index_read > 94 && Story.index_read < 117)
        {
            Story.index_skip = 117; //5月おうじの選択肢に飛ぶ
            Story.index_read = 117;
        }
        else if (Story.index_read > 109 && Story.index_read < 141)
        {
            Story.index_skip = 141; //6月に飛ぶ
            Story.index_read = 141;
        }
        else if (Story.index_read > 124 && Story.index_read < 154)
        {
            Story.index_skip = 154; //6月おうじの選択肢に飛ぶ
            Story.index_read = 154;
        }
        else if (Story.index_read > 137 && Story.index_read < 190)
        {
            Story.index_skip = 190; //7月に飛ぶ
            Story.index_read = 190;
        }
        else if (Story.index_read > 171 && Story.index_read < 208)
        {
            Story.index_skip = 208; //7月おうじに飛ぶ
            Story.index_read = 208;
        }
        else if (Story.index_read > 188 && Story.index_read < 230)
        {
            Story.index_skip = 230; //7月ひかるに飛ぶ
            Story.index_read = 230;
        }
        else if (Story.index_read > 222 && Story.index_read < 261)
        {
            Story.index_skip = 262; //10月に飛ぶ
            Story.index_read = 262;
        }
        else if (Story.index_read > 262 && Story.index_read < 277)
        {
            Story.index_skip = 287; //11月に飛ぶ
            Story.index_read = 287;
        }
    }

    public void onClicked_backButton()
    {
        SkipselectPanel.SetActive(false);
        Screenbutton.SetActive(true);
    }
}
