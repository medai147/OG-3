using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator ScreenAnimator; // Animatorをアタッチ
    [SerializeField] private GameObject ScreenPanel;

    public void PlayScreenAnimation(string screenType, System.Action onComplete = null)
    {
        if (ScreenAnimator == null)
        {
            Debug.LogError("Screen Animator が設定されていません！");
            return;
        }


        if (screenType != "")
        {
            ScreenPanel.SetActive(true);
            ScreenAnimator.SetTrigger(screenType);
        }
        else
        {
            ScreenPanel.SetActive(false);
            Debug.LogWarning("無効なフェードタイプが指定されました: " + screenType);
        }
    }
}
