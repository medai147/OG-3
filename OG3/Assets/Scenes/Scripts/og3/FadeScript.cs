using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float speed = 0.0001f; //“§–¾‰»‚Ì‘¬‚³
    float alfa;
    float red, green, blue;
    public static bool isFadeIn = false;
    public static bool isFadeOut = false;
    [SerializeField] GameObject ScreenButton;
    [SerializeField] GameObject fadePanel;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadeOut == true) //ˆÃ‚­‚È‚é
        {
            fadePanel.SetActive(true);
            ScreenButton.SetActive(false);
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
            count++;
            //Debug.Log("count:" + count + ", alfa:" + alfa);
            if (count > 300)
            {
                isFadeOut = false;
                alfa = 1;
                //isFadeIn = true;
                ScreenButton.SetActive(true);
                //Story.isFade = true;
            }
        } else if (isFadeIn == true) {  //–¾‚é‚­‚È‚é
            fadePanel.SetActive(true);
            ScreenButton.SetActive(false);
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            if (count == 0)
            {
                alfa = 1;
            }
            alfa -= speed;
            count++;
            //Debug.Log("count:"+count + ", alfa:" + alfa);
            if (count > 500)
            {
                GetComponent<Image>().color = new Color(red, green, blue, 0);
                isFadeOut = false;
                isFadeIn = false;
                ScreenButton.SetActive(true);
            }
        }
        else if (isFadeOut == false && isFadeIn == false)
        {
            count = 0;
            //GetComponent<Image>().color = new Color(red, green, blue, 0);
        }

    }

}
