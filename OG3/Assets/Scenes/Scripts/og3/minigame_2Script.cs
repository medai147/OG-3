using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigame_2Script : MonoBehaviour
{
    string ice1 = "";
    string ice2 = "";
    string ice3 = "";

    string ichigo = "いちごアイス";
    string greentea = "まっちゃアイス";
    string chocolate = "チョコレートアイス";

    string table1_order = "";
    string table2_order = "";
    string table3_order = "";

    int ice1_Flavor = 0; // 0:アイス未定義
    int ice2_Flavor = 0; // 0:アイス未定義
    int ice3_Flavor = 0; // 0:アイス未定義

    public int roundCounter = 1;

    string order = "";

    string result = "";

    //int show = 1;

    int correct = 0;
    int wrong = 0;

    int iceCount_order;
    int iceCount_serve = 0;

    int tableNumber_order;
    int tableNumber_serve = 0;

    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject gamePanel1;
    [SerializeField] GameObject gamePanel2;
    [SerializeField] GameObject resultPanel;

    [SerializeField] GameObject orderText;
    [SerializeField] GameObject resultText;

    [SerializeField] GameObject iceCountText;

    [SerializeField] GameObject resultPanel2;

    // Start is called before the first frame update
    void Start()
    {
        startpanel.SetActive(true);
        gamePanel1.SetActive(false);
        gamePanel2.SetActive(false);
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void counter()
    {
        roundCounter++;
        gamePanel2.SetActive(true);
        gamePanel1.SetActive(false);
    }

    public void ice1_button()
    {
        Debug.Log("pushed! ice1_Flavor is " + ice1_Flavor);
        if (iceCount_serve == 0)
        {
            if (ice1_Flavor == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel2.SetActive(false);
        resultText.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
    }

    public void ice2_button()
    {
        if (iceCount_serve == 0)
        {
            if (ice1_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel2.SetActive(false);
        resultText.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
    }

    public void ice3_button()
    {
        if (iceCount_serve == 0)
        {
            if (ice1_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
                result = "正解！";
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
                result = "間違い！";
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel2.SetActive(false);
        resultText.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
    }

    public void startbutton()
    {
        gamePanel1.SetActive(true);
        startpanel.SetActive(false);
        orderDispbutton();
    }

    public void orderDispbutton()
    {
        //if (show == 1)
        //{
        iceCount_order = 1;
        int ice1_flavor_rnd = Random.Range(1, 4);
        if (ice1_flavor_rnd == 1)
        {
            ice1 = ichigo;
            ice1_Flavor = 1; // 1:いちごアイス
        }
        else if (ice1_flavor_rnd == 2)
        {
            ice1 = greentea;
            ice1_Flavor = 2; // 2:まっちゃアイス
        }
        else
        {
            ice1 = chocolate;
            ice1_Flavor = 3; // 3:チョコレートアイス
        }

        int tableNumber_rnd_1 = Random.Range(1, 7);
        table1_order = tableNumber_rnd_1 + "番テーブル";

        if (roundCounter > 1)
        {
            iceCount_order = 2;
            int ice2_flavor_rnd = Random.Range(1, 4);
            if (ice2_flavor_rnd == 1)
            {
                ice2 = ichigo;
                ice2_Flavor = 1; // 1:いちごアイス
            }
            else if (ice2_flavor_rnd == 2)
            {
                ice2 = greentea;
                ice2_Flavor = 2; // 2:まっちゃアイス
            }
            else
            {
                ice2 = chocolate;
                ice2_Flavor = 3; // 3:チョコレートアイス
            }

            int tableNumber_rnd_2 = Random.Range(1, 7);
            table2_order = tableNumber_rnd_2 + "番テーブル";
        }

        if (roundCounter > 3)
        {
            iceCount_order = 3;
            int ice3_flavor_rnd = Random.Range(1, 4);
            if (ice3_flavor_rnd == 1)
            {
                ice3 = ichigo;
                ice3_Flavor = 1; // 1:いちごアイス
            }
            else if (ice3_flavor_rnd == 2)
            {
                ice3 = greentea;
                ice3_Flavor = 2; // 2:まっちゃアイス
            }
            else
            {
                ice3 = chocolate;
                ice3_Flavor = 3; // 3:チョコレートアイス
            }

            int tableNumber_rnd_3 = Random.Range(1, 7);
            table3_order = tableNumber_rnd_3 + "番テーブル";
        }

        order = ice1 + ", " + table1_order + "\n" + ice2 + table2_order + "\n" + ice3 + "\n" + "(" + roundCounter + ")";
        Debug.Log(order);

        orderText.GetComponent<Text>().text = order;
    }

    public void next()
    {
        Debug.Log("iceCount_serve:" + iceCount_serve + ", iceCount_order" + iceCount_order + "★next★");
        if(iceCount_serve == iceCount_order)
        {
            iceCountText.GetComponent<Text>().text = "1個目";
            resultPanel.SetActive(false);
            startbutton();
            iceCount_serve = 0;
        } else
        {
            iceCountText.GetComponent<Text>().text = (iceCount_serve + 1) + "個目";
            gamePanel2.SetActive(true);
            resultPanel.SetActive(false);
        }
    }

    public void iceCount_serve_in()
    {
        iceCount_serve++;
    }

    public void retrybutton()
    {

    }

    public void tomenubutton()
    {

    }
}