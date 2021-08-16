using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class countersclipt : MonoBehaviour
{
    private int ice;
    private int score;
    private int finishcount;
    private static int nowcoin;
    public Text scoretext;
    public Text icetext;
    public Text finishscoretext;
    public Text getcointext;
    public Text cointext;
    public Text startcointext;
    [SerializeField] GameObject finishpanel;
    // Start is called before the first frame update
    void Start()
    {
        ice =  Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        startcointext.text = "所持金:" + nowcoin;
        scoretext.text = "score:" + score;
        if (finishpanel.activeSelf == true)
        {
            finishcount++;
        } else
        {
            finishcount = 0;
        }
        if(finishcount == 1)
        {
            finishscore();
        }

       // Debug.Log("bbb");
       if(ice == 1)
        {
            icetext.text = "ストロベリー";
        } else if (ice == 2)
        {
            icetext.text = "抹茶";
        } else if(ice == 3)
        {
            icetext.text = "チョコレート";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(ice == 1 && other.name == "pinkImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        }
        else if(ice == 2 && other.name == "greenImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        } else if(ice == 3 && other.name == "brownImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        }else
        {
            score -= 1;
            ice = Random.Range(1, 4);
        }
    }

    private void finishscore()
    {
        finishscoretext.text = score.ToString();
        if (score > 30 && finishpanel.activeSelf == true)
        {
            getcointext.text = "30コインゲット！";
            nowcoin += 30;
            score = 0;

        }
        else if (score > 25 && finishpanel.activeSelf == true)
        {
            getcointext.text = "25コインゲット！";
            nowcoin += 25;
            score = 0;
        }
        else if (score > 20 && finishpanel.activeSelf == true)
        {
            getcointext.text = "20コインゲット！";
            nowcoin += 20;
            score = 0;
        }
        else if (finishpanel.activeSelf == true && score >= 0)
        {
            getcointext.text = "給料無し！";
        }
        cointext.text = "所持金:" + nowcoin;
    }
}
