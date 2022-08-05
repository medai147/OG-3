using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class albumscript : MonoBehaviour
{
    [SerializeField] GameObject[] albumcolumn = new GameObject[6];
    [SerializeField] Sprite[] still = new Sprite[6];
    [SerializeField] GameObject fullimage;
    [SerializeField] GameObject backbutton;
    // Start is called before the first frame update
    void Start()
    {
      for(int i = 0; i < GameManager.instance.getimage.Length; i++)
        {
            if(GameManager.instance.getimage[i] == true)
            {
                albumcolumn[i].GetComponent<Image>().sprite = still[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClicked_albumclick(int num)
    {
        switch (num)
        {
            case 0:
                click(num);
                break;
            case 1:
                click(num);
                break;
            case 2:
                click(num);
                break;
            case 3:
                click(num);
                break;
            case 4:
                click(num);
                break;
            case 5:
                click(num);
                break;
        }
    }

    void click(int num)
    {
        if (GameManager.instance.getimage[num])
        {
            fullimage.SetActive(true);
            backbutton.SetActive(false);
            fullimage.GetComponent<Image>().sprite = albumcolumn[num].GetComponent<Image>().sprite;
        }
    }

    public void onClicked_fullimage()
    {
        backbutton.SetActive(true);
        fullimage.SetActive(false);
    }
}
