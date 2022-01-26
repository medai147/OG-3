using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveanimationsclipt : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] GameObject text;
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
    }

    // Update is called once per frame
    void Update()
    {
        imagemove();
        textmove();
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

        if (count > 300)
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
        if (textrectTransform.sizeDelta.y < 40 && bigmove_text == true)
        {
            textrectTransform.sizeDelta += new Vector2(0, +bigspeed_text) * Time.deltaTime;
        }
        if(textrectTransform.sizeDelta.y > 40)
        {
            bigmove_text = false;
        }
        if (count > 300)
        {
            textrectTransform.sizeDelta += new Vector2(0, -smallspeed_text) * Time.deltaTime;
        }
    }
}

