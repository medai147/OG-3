using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class albumscript : MonoBehaviour
{
    [SerializeField] GameObject[] albumcolumn = new GameObject[6];
    [SerializeField] Sprite[] still = new Sprite[12]; // 全部で12枚のスチル
    [SerializeField] GameObject fullimage;
    [SerializeField] GameObject backbutton;
    [SerializeField] GameObject nextbutton; // 次のページボタン
    [SerializeField] GameObject prevbutton; // 前のページボタン

    private int currentPage = 0; // 現在のページ番号

    void Start()
    {
        updateAlbum();
    }

    void updateAlbum()
    {
        int startIdx = currentPage * 6;

        // 6つのスチルを更新
        for (int i = 0; i < 6; i++)
        {
            int idx = startIdx + i;
            if (idx < GameManager.instance.getimage.Length)
            {
                if (GameManager.instance.getimage[idx] == 1)
                {
                    albumcolumn[i].GetComponent<Image>().sprite = still[idx];
                    albumcolumn[i].SetActive(true);
                }
                else
                {
                    albumcolumn[i].GetComponent<Image>().sprite = null;
                    albumcolumn[i].SetActive(false);
                }
            }
            else
            {
                albumcolumn[i].SetActive(false);
            }
        }

        // ボタンの有効/無効を切り替える
        prevbutton.SetActive(currentPage > 0);
        nextbutton.SetActive(startIdx + 6 < still.Length);
    }

    public void onClicked_albumclick(int num)
    {
        int realIndex = currentPage * 6 + num;
        if (realIndex < GameManager.instance.getimage.Length && GameManager.instance.getimage[realIndex] == 1)
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

    public void onClicked_next()
    {
        currentPage++;
        updateAlbum();
    }

    public void onClicked_prev()
    {
        currentPage--;
        updateAlbum();
    }
}
