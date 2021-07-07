using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class countersclipt : MonoBehaviour
{
    private int ice;
    private int score;
    public Text scoretext;
    public Text icetext;
    // Start is called before the first frame update
    void Start()
    {
        ice =  Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
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
        scoretext.text = "score:" + score;
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
}
