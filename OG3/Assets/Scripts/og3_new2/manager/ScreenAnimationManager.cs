using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator ScreenAnimator; // Animator���A�^�b�`
    [SerializeField] private GameObject ScreenPanel;

    public void PlayScreenAnimation(string screenType, System.Action onComplete = null)
    {
        if (ScreenAnimator == null)
        {
            Debug.LogError("Screen Animator ���ݒ肳��Ă��܂���I");
            return;
        }


        if (screenType != "")
        {
            ScreenAnimator.CrossFade("New State", 0.1f);
            ScreenPanel.SetActive(true);
            ScreenAnimator.SetTrigger(screenType);
        }
        else
        {
            ScreenAnimator.CrossFade("New State", 0.1f);
            ScreenPanel.SetActive(false);
            Debug.LogWarning("�����ȃt�F�[�h�^�C�v���w�肳��܂���: " + screenType);
        }
    }
}
