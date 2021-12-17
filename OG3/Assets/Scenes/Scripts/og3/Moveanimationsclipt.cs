using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveanimationsclipt : MonoBehaviour
{
    [SerializeField] float smallspeed = 200;
    [SerializeField] float bigspeed = 250;
    RectTransform rectTransform;
    int count;
    bool bigmove = true;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(247.2563f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform.sizeDelta.y < 50 && bigmove == true)
        {
            rectTransform.sizeDelta += new Vector2(0, +bigspeed) * Time.deltaTime;
        }

        if(rectTransform.sizeDelta.y > 50)
        {
            count++;
            bigmove = false;
        }

        if (count > 200)
        {
            rectTransform.sizeDelta += new Vector2(0, -smallspeed) * Time.deltaTime;
        }
        
        if(rectTransform.sizeDelta.y < 0)
        {
            Destroy(gameObject);
            count = 0;
            bigmove = true;
        }
    }
}
