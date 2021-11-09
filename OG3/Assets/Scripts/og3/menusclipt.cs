using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menusclipt : MonoBehaviour
{
    [SerializeField] GameObject logPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject Screenbutton;
    [SerializeField] GameObject TextImage;
    [SerializeField] GameObject MainTextPanel;
    [SerializeField] GameObject NameTextPanel;
    [SerializeField] GameObject monthtext;
    [SerializeField] GameObject SelectButtonPanel;
    [SerializeField] GameObject deleteResetButton;
    [SerializeField] GameObject SkipselectPanel;
    [SerializeField] GameObject SkipselectpanelText;
    [SerializeField] GameObject cannotskipAlertPanel;
    [SerializeField] GameObject monthimage;
    [SerializeField] GameObject menubutton;
    private Text skiptext;
    private int selectState;
    private int alertdisp;

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

    public void onClicked_log()
    {
        logPanel.SetActive(true);
        MenuPanel.SetActive(false);
        Screenbutton.SetActive(false);
    }

    public void onClicked_delete()
    {
        TextImage.SetActive(false);
        MainTextPanel.SetActive(false);
        NameTextPanel.SetActive(false);
        monthtext.SetActive(false);
        Screenbutton.SetActive(false);
        menubutton.SetActive(false);
        if (SelectButtonPanel.activeSelf == true) {
            //Debug.Log("選択肢あり");
            SelectButtonPanel.SetActive(false);
        }
        MenuPanel.SetActive(false);
        deleteResetButton.SetActive(true);
    }

    public void onClicked_deleteResetButton()
    {

        TextImage.SetActive(true);
        MainTextPanel.SetActive(true);
        NameTextPanel.SetActive(true);
        monthtext.SetActive(true);
        Screenbutton.SetActive(true);
        menubutton.SetActive(true);
        selectState = Story.selectpanelState;
        if (selectState == 1)
        {
            SelectButtonPanel.SetActive(true);
            Screenbutton.SetActive(false);
        }
        deleteResetButton.SetActive(false);
    }

    public void onClicked_skip()
    {
        if ((SelectButtonPanel.activeSelf == true || monthimage.activeSelf == true || Story.index_read >= 65 && Story.index_read < 94) && cannotskipAlertPanel.activeSelf != true)
        {
            cannotskipAlertPanel.SetActive(true);
            Invoke("deleteAlertPanel", 2.0f);
        }
        else if (Story.index_skip != 49 && Story.index_skip != 66)
        {
            MenuPanel.SetActive(false);
            Screenbutton.SetActive(false);
            SkipselectPanel.SetActive(true);
            if (Story.index_read < 49) 
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "4月をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 49 && Story.index_read < 65)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "5月(光と勉強)をスキップしますか？";
                skiptext.text += "\n";
                skiptext.text += "(選択肢のところまで)";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 66 && Story.index_read < 94)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "5月(光と勉強)をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 94 && Story.index_read < 107)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "5月(おうじからのDM)をスキップしますか？";
                skiptext.text += "\n";
                skiptext.text += "(選択肢のところまで)";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 107 && Story.index_read < 124)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "5月(おうじからのDM)をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 124 && Story.index_read < 137)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "6月(おうじからのDM)をスキップしますか？";
                skiptext.text += "\n";
                skiptext.text += "(選択肢のところまで)";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 137 && Story.index_read < 171)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "6月(おうじからのDM)をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 171 && Story.index_read < 188)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "7月(体育祭の道具運び)をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
            else if (Story.index_read > 188 && Story.index_read < 210)
            {
                //SkipselectpanelText.SetActive(true);
                skiptext = GameObject.Find("SkipselectpanelText").GetComponent<Text>();
                skiptext.text = "7月(リレーのアンカー)をスキップしますか？";
                SkipselectpanelText.SetActive(true);
            }
        }
    }

    private void deleteAlertPanel()
    {
        delete_cannnotskiptextImage();
    }

    public void delete_cannnotskiptextImage()
    {
        cannotskipAlertPanel.SetActive(false);
    }
}
