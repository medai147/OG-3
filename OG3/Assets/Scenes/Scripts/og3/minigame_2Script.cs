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

    string table1_order_str = "";
    string table2_order_str = "";
    string table3_order_str = "";

    int table1_order_num = 0;
    int table2_order_num = 0;
    int table3_order_num = 0;

    int ice1_Flavor = 0; // 0:アイス未定義
    int ice2_Flavor = 0; // 0:アイス未定義
    int ice3_Flavor = 0; // 0:アイス未定義

    public int roundCounter = 1;
    public int dispCount = 0;

    string order = "";

    string result = "";

    int gamestate = 0;

    int correct = 0;
    int wrong = 0;

    public int iceCount_order;
    public int iceCount_serve = 0;

    public int tableCount_order;
    public int tableCount_serve = 0;

    public int total;

    [SerializeField] GameObject startpanel;
    [SerializeField] GameObject gamePanel1;
    [SerializeField] GameObject gamePanel2;
    [SerializeField] GameObject gamePanel3;
    [SerializeField] GameObject resultPanel;

    [SerializeField] GameObject orderText;
    [SerializeField] GameObject resultText;
    [SerializeField] GameObject explainText;

    [SerializeField] GameObject iceCountText;

    [SerializeField] GameObject resultPanel2;

    private float totalTime = 30;
    int seconds = 30;
    [SerializeField] GameObject timerText;
    [SerializeField] GameObject resultText1;

    // Start is called before the first frame update
    void Start()
    {
        startpanel.SetActive(true);
        gamePanel1.SetActive(false);
        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamestate == 1)
        {
            startTimer();
        }
        if(seconds == 0)
        {
            gamestate = 0;
            resultPanel2.SetActive(true);
        }
        if(resultPanel2.activeSelf)
        {
            if(correct >= 15)
            {
                resultText.GetComponent<Text>().text = "30円ゲット！" + correct + "問正解！";
            }
            else
            {
                resultText.GetComponent<Text>().text = "失敗…　給料無し！";
            }
        }
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
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel3.SetActive(true);
    }

    public void ice2_button()
    {
        if (iceCount_serve == 0)
        {
            if (ice1_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel3.SetActive(true);
    }

    public void ice3_button()
    {
        if (iceCount_serve == 0)
        {
            if (ice1_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 1)
        {
            if (ice2_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }
        else if (iceCount_serve == 2)
        {
            if (ice3_Flavor == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("iceCount_serve:" + iceCount_serve);
        }

        gamePanel3.SetActive(true);
    }

    public void startbutton()
    {
        gamePanel1.SetActive(true);
        startpanel.SetActive(false);
        orderDispbutton();
        gamestate = 1;
    }

    public void orderDispbutton()
    {
        dispCount++;
        //if (show == 1)
        //{
        iceCount_order = 1;
        tableCount_order = 1;
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
        table1_order_str = tableNumber_rnd_1 + "番テーブル";

        if(tableNumber_rnd_1 == 1)
        {
            table1_order_num = 1;
        } 
        else if(tableNumber_rnd_1 == 2) 
        {
            table1_order_num = 2;
        }
        else if(tableNumber_rnd_1 == 3)
        {
            table1_order_num = 3;
        }
        else if (tableNumber_rnd_1 == 4)
        {
            table1_order_num = 4;
        }
        else if (tableNumber_rnd_1 == 5)
        {
            table1_order_num = 5;
        }
        else
        {
            table1_order_num = 6;
        }

        if (roundCounter > 1)
        {
            total++;
            iceCount_order = 2;
            tableCount_order = 2;
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
            table2_order_str = ", " + tableNumber_rnd_2 + "番テーブル";

            if (tableNumber_rnd_2 == 1)
            {
                table2_order_num = 1;
            }
            else if (tableNumber_rnd_2 == 2)
            {
                table2_order_num = 2;
            }
            else if (tableNumber_rnd_2 == 3)
            {
                table2_order_num = 3;
            }
            else if (tableNumber_rnd_2 == 4)
            {
                table2_order_num = 4;
            }
            else if (tableNumber_rnd_2 == 5)
            {
                table2_order_num = 5;
            }
            else
            {
                table2_order_num = 6;
            }
        }

        if (roundCounter > 3)
        {
            total++;
            iceCount_order = 3;
            tableCount_order = 3;
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
            table3_order_str = ", " + tableNumber_rnd_3 + "番テーブル";

            if (tableNumber_rnd_3 == 1)
            {
                table3_order_num = 1;
            }
            else if (tableNumber_rnd_3 == 2)
            {
                table3_order_num = 2;
            }
            else if (tableNumber_rnd_3 == 3)
            {
                table3_order_num = 3;
            }
            else if (tableNumber_rnd_3 == 4)
            {
                table3_order_num = 4;
            }
            else if (tableNumber_rnd_3 == 5)
            {
                table3_order_num = 5;
            }
            else
            {
                table3_order_num = 6;
            }
        }
        total++;

        order = ice1 + ", " + table1_order_str + "\n" + ice2 + table2_order_str + "\n" + ice3 + table3_order_str + "\n" + "(" + roundCounter + ")";
        Debug.Log(order);

        orderText.GetComponent<Text>().text = order;
    }

    public void iceCount_serve_in()
    {
        iceCount_serve++;
    }

    public void retrybutton()
    {
        resultPanel2.SetActive(false);
        roundCounter = 1;
        dispCount = 0;
        iceCount_order = 0;
        iceCount_serve = 0;
        tableCount_order = 0;
        tableCount_serve = 0;
        wrong = 0;
        correct = 0;
        table1_order_str = "";
        table2_order_str = "";
        table3_order_str = "";

        table1_order_num = 0;
        table2_order_num = 0;
        table3_order_num = 0;

        ice1_Flavor = 0; // 0:アイス未定義
        ice2_Flavor = 0; // 0:アイス未定義
        ice3_Flavor = 0; // 0:アイス未定義

        ice1 = "";
        ice2 = "";
        ice3 = "";
        totalTime = 30;
        startbutton();
    }

    public void tomenubutton()
    {

    }

    public void tableCount_serve_in()
    {
        tableCount_serve++;
    }

    public void table1_button()
    {
        Debug.Log("pushed! 1番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            if(wrong == 0)
            {
                result = "正解!";
            } else
            {
                result = "間違い！";
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 1)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void table2_button()
    {
        Debug.Log("pushed! 2番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 2)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void table3_button()
    {
        Debug.Log("pushed! 3番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 3)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void table4_button()
    {
        Debug.Log("pushed! 4番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 4)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 4)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 4)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void table5_button()
    {
        Debug.Log("pushed! 5番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 5)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 5)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 5)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void table6_button()
    {
        Debug.Log("pushed! 6番テーブル");
        if (tableCount_serve == 0)
        {
            if (table1_order_num == 6)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 1)
        {
            if (table2_order_num == 6)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }
        else if (tableCount_serve == 2)
        {
            if (table3_order_num == 6)
            {
                correct++;
                Debug.Log("correct:" + correct);
            }
            else
            {
                wrong++;
                Debug.Log("wrong:" + wrong);
            }
            Debug.Log("tableCount_serve:" + tableCount_serve);
            if (wrong == 0)
            {
                result = "正解!";
            }
            else
            {
                result = "間違い！";
            }
        }

        gamePanel2.SetActive(false);
        gamePanel3.SetActive(false);
        resultText1.GetComponent<Text>().text = result;
        resultPanel.SetActive(true);
        next();
    }

    public void next()
    {
        tableCount_serve_in();
        Debug.Log("iceCount_serve:" + iceCount_serve + ", iceCount_order:" + iceCount_order + "★next★" +"\n" + "tableCount_serve:" + tableCount_serve + ", tableCount_order:" + tableCount_order);
        if (tableCount_serve == tableCount_order) //(iceCount_serve == iceCount_order) &&
        {
            if(dispCount == 5)
            {
                resultPanel2.SetActive(true);
            }
            iceCountText.GetComponent<Text>().text = "1個目";
            //resultPanel.SetActive(false);
            startbutton();
            iceCount_serve = 0;
            tableCount_serve = 0;
            wrong = 0;
        }
        else
        {
            iceCountText.GetComponent<Text>().text = (iceCount_serve + 1) + "個目";
            explainText.GetComponent<Text>().text = "注文されたテーブルを選択";
            gamePanel2.SetActive(true);
            //resultPanel.SetActive(false);
            wrong = 0;
        }
    }

    public void startTimer()
    {
        totalTime -= Time.deltaTime;
        seconds = (int)totalTime;
        timerText.GetComponent<Text>().text = seconds.ToString();
    }
}