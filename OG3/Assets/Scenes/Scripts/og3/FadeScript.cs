using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float speed = 0.001f; //“§–¾‰»‚Ì‘¬‚³
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
        if (isFadeOut == true)
        {
            fadePanel.SetActive(true);
            ScreenButton.SetActive(false);
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa += speed;
            count++;
            if (count > 400)
            {
                isFadeOut = false;
                //isFadeIn = true;
                ScreenButton.SetActive(true);
                Story.isFade = true;
            }
        } else if (isFadeIn == true) {
            if (count == 0)
            {
                alfa = 255;
            }
            fadePanel.SetActive(true);
            ScreenButton.SetActive(false);
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            alfa -= speed;
            count++;
            if (count > 400)
            {
                //isFadeOut = false;
                isFadeIn = false;
                ScreenButton.SetActive(true);
            }
        }
        else if (isFadeOut == false && isFadeIn == false)
        {
            count = 0;
            GetComponent<Image>().color = new Color(red, green, blue, 0);
        }
    }
}
