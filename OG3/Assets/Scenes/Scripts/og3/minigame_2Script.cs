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

    int ice1_Flavor = 0; // 0:アイス未定義
    int ice2_Flavor = 0; // 0:アイス未定義
    int ice3_Flavor = 0; // 0:アイス未定義

    int roundCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        int ice1_flavor_rnd = Random.Range(1, 4);
        if(ice1_flavor_rnd == 1)
        {
            ice1 = ichigo;
            ice1_Flavor = 1; // 1:いちごアイス
        } else if(ice1_flavor_rnd == 2)
        {
            ice1 = greentea;
            ice1_Flavor = 2; // 2:まっちゃアイス
        } else
        {
            ice1 = chocolate;
            ice1_Flavor = 3; // 3:チョコレートアイス
        }

        if(roundCounter > 5)
        {
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
                ice3 = chocolate;
                ice3_Flavor = 3; // 3:チョコレートアイス
            }
        }

        if (roundCounter > 10)
        {
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = ice1;
        gameObject.GetComponent<Text>().text += "\n" + ice2;
        gameObject.GetComponent<Text>().text += "\n" + ice3;

        gameObject.GetComponent<Text>().text += "\n" + roundCounter.ToString();

    }

    public void counter()
    {
        roundCounter++;
    }
}
