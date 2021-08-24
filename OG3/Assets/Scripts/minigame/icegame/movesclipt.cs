using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class movesclipt : MonoBehaviour
{
    public Image pink_image;
    public Image green_image;
    public Image brown_image;
    public Vector2 MousePos;
    public Canvas canvas;
    public static bool hit;
    //キャンバス内のレクトトランスフォーム
    public RectTransform canvasRect;

    // Start is called before the first frame update
    void Start()
    {
        pink_image = GameObject.Find("pinkImagemove").GetComponent<Image>();
        green_image = GameObject.Find("greenImagemove").GetComponent<Image>();
        brown_image = GameObject.Find("brownImagemove").GetComponent<Image>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hit);
    }

    public void Pinkicedown()
    {
        Debug.Log("ピンク");
    }

    public void Greenicedown()
    {
        Debug.Log("緑");
    }

    public void Brownicedown()
    {
        Debug.Log("茶色");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        hit = true;
        Debug.Log("当たってるよ");

    }

    public void Pinkicedrag()
    {
        if (hit == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
            Input.mousePosition, canvas.worldCamera, out MousePos);
            pink_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, MousePos.y);
        } else
        {
            pink_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(-244.6f, -82.9f);
        }
    }

    public void Greenicedrag()
    {
        if (hit == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
            Input.mousePosition, canvas.worldCamera, out MousePos);
            green_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, MousePos.y);
        }
        else
        {
            green_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(9.4f, -87.6f);
        }
    }

    public void Brownicedrag()
    {
        if (hit == false)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
            Input.mousePosition, canvas.worldCamera, out MousePos);
            brown_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(MousePos.x, MousePos.y);
        }
        else
        {
            brown_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(244, -98.428f);
        }
    }
    public void Pinkicedrop()
    {
        pink_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(-244.6f, -82.9f);
        hit = false;
    }

    public void Greenicedrop()
    {
        green_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(9.4f, -87.6f);
        hit = false;
    }

    public void Brownicedrop()
    {
        brown_image.GetComponent<RectTransform>().anchoredPosition = new Vector2(244, -98.428f);
        hit = false;
    }



}
