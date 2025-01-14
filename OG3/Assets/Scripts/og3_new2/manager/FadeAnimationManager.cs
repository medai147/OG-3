using UnityEngine;

public class FadeAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator; // Animatorをアタッチ
    [SerializeField] private GameObject FadePanel;
    private System.Action onFadeComplete; // フェード完了時のコールバック

    /// <summary>
    /// フェードアニメーションを再生
    /// </summary>
    /// <param name="fadeType">"in" または "out" を指定</param>
    /// <param name="onComplete">フェード完了後に呼び出すアクション</param>
    public void PlayFadeAnimation(string fadeType, System.Action onComplete = null)
    {
        if (fadeAnimator == null)
        {
            Debug.LogError("Fade Animator が設定されていません！");
            return;
        }

        onFadeComplete = onComplete; // コールバックを登録

        if (fadeType == "in")
        {
            FadePanel.SetActive(true);
            fadeAnimator.SetTrigger("FadeIn");
        }
        else if (fadeType == "out")
        {
            FadePanel.SetActive(true);
            fadeAnimator.SetTrigger("FadeOut");
        }
        else
        {
            Debug.LogWarning("無効なフェードタイプが指定されました: " + fadeType);
        }
    }

    /// <summary>
    /// フェードアニメーション完了時に呼び出されるメソッド
    /// </summary>
    public void OnFadeComplete()
    {
        FadePanel.SetActive(false);
        onFadeComplete?.Invoke(); // コールバックを実行
    }
}
