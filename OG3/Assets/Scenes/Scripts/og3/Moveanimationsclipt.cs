using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moveanimationsclipt : MonoBehaviour
{
    private int movetxtnum;
    [SerializeField] GameObject image;
    [SerializeField] Text text;
    [SerializeField] float smallspeed = 200;
    [SerializeField] float bigspeed = 250;
    [SerializeField] float smallspeed_text = 200;
    [SerializeField] float bigspeed_text = 250;
    RectTransform imagerectTransform;
    RectTransform textrectTransform;
    int count;
    bool bigmove = true;
    bool bigmove_text = true;
    // Start is called before the first frame update
    void Start()
    {
        imagerectTransform = image.GetComponent<RectTransform>();
        imagerectTransform.sizeDelta = new Vector2(247.2563f, 0);
        textrectTransform = text.GetComponent<RectTransform>();
        textrectTransform.sizeDelta = new Vector2(100, 0);
        //text = GameObject.Find("movetext").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        imagemove();
        textmove();
        textchange();
    }


    public void imagemove()
    {
        if (imagerectTransform.sizeDelta.y < 70 && bigmove == true)
        {
            imagerectTransform.sizeDelta += new Vector2(0, +bigspeed) * Time.deltaTime;
        }

        if (imagerectTransform.sizeDelta.y > 70)
        {
            count++;
            bigmove = false;
        }

        if (count > 150)
        {
            imagerectTransform.sizeDelta += new Vector2(0, -smallspeed) * Time.deltaTime;
        }
        
        if (imagerectTransform.sizeDelta.y < 0)
        {
            if(gameObject)
            {
                Destroy(gameObject);
            }
            count = 0;
            bigmove = true;
            bigmove_text = true;
        }
    }

    public void textmove()
    {
        if (imagerectTransform.sizeDelta.y >= 0)
        {
            if (textrectTransform.sizeDelta.y < 40 && bigmove_text == true)
            {
                textrectTransform.sizeDelta += new Vector2(0, +bigspeed_text) * Time.deltaTime;
            }
            if (textrectTransform.sizeDelta.y > 40)
            {
                bigmove_text = false;
            }
            if (count > 150)
            {
                textrectTransform.sizeDelta += new Vector2(0, -smallspeed_text) * Time.deltaTime;
            }
            
        }
        
    }

    public void textchange()
    {
        if (gameObject)
        {
            movetxtnum = int.Parse(PlayerPrefs.GetString("MOVETEXT"));
            if (movetxtnum == 1)
            {
                text.text = "商店街";
            }
            else if (movetxtnum == 2)
            {
                text.text = "ファンシーショップ";
            }
            else if (movetxtnum == 3)
            {
                text.text = "教室";
            }
            else if (movetxtnum == 4)
            {
                text.text = "部屋";
            }
            else if (movetxtnum == 5)
            {
                text.text = "倉庫";
            }
            else if (movetxtnum == 6)
            {
                text.text = "保健室";
            }
        }
    }
}

