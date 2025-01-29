using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class albumscript : MonoBehaviour
{
    [SerializeField] GameObject[] albumcolumn = new GameObject[6];
    [SerializeField] Sprite[] still = new Sprite[12]; // �S����12���̃X�`��
    [SerializeField] GameObject fullimage;
    [SerializeField] GameObject backbutton;
    [SerializeField] GameObject nextbutton; // ���̃y�[�W�{�^��
    [SerializeField] GameObject prevbutton; // �O�̃y�[�W�{�^��

    private int currentPage = 0; // ���݂̃y�[�W�ԍ�

    void Start()
    {
        updateAlbum();
    }

    void updateAlbum()
    {
        int startIdx = currentPage * 6;

        // 6�̃X�`�����X�V
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

        // �{�^���̗L��/������؂�ւ���
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
