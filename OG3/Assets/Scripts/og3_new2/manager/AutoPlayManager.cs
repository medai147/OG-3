using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayManager : MonoBehaviour
{
    private bool isAutoPlaying = false; // オート再生中かどうか
    private Coroutine autoPlayCoroutine;
    private StorySystem storySystem;
    [SerializeField] private Button autoPlayButton;
    [SerializeField] private Button nextButton; // 次のボタン

    // デフォルトとオート中の色を定義
    private readonly Color defaultColor = new Color32(50, 50, 50, 255); // #323232
    private readonly Color autoColor = new Color32(255, 57, 222, 255);  // #FF39DE

    [SerializeField] private Text autoModeText; // オートモードの状態を表示するテキスト
    public void Initialize(StorySystem storySystem)
    {
        this.storySystem = storySystem;

        if (this.storySystem == null)
        {
            Debug.LogError("StorySystem が初期化されていません。");
        }
    }


    public void ToggleAutoPlay()
    {
        Debug.Log("押せた");
        if (isAutoPlaying)
        {
            StopAutoPlay();
        }
        else
        {
            StartAutoPlay();
        }
        UpdateAutoModeTextColor(); // テキストの色を更新
        UpdateNextButtonState();  // nextButton の状態を更新
    }

    private void StartAutoPlay()
    {
        isAutoPlaying = true;
        autoPlayCoroutine = StartCoroutine(AutoPlayCoroutine());
    }

    private void StopAutoPlay()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
        isAutoPlaying = false;
    }

    private IEnumerator AutoPlayCoroutine()
    {
        while (isAutoPlaying)
        {
            // テキストが表示完了するまで待機
            while (!storySystem.uiManager.IsTextFullyDisplayed)
            {
                yield return null; // 次のフレームまで待機
            }

            // 表示完了後、一定時間待機
            yield return new WaitForSeconds(GetAutoPlayDelay());

            // 次のストーリーへ進む
            storySystem.NextStory();
        }
    }


    private float GetAutoPlayDelay()
    {
        // 設定値に応じた遅延時間を取得
        switch (SettingManager.Instance.AutoSpeed)
        {
            case 1: return 2.0f; // 遅い
            case 2: return 1.0f; // 中速
            case 3: return 0.5f; // 速い
            default: return 1.0f;
        }
    }

    public void SetAutoPlayButtonAction(UnityEngine.Events.UnityAction action)
    {
        autoPlayButton.onClick.RemoveAllListeners();
        autoPlayButton.onClick.AddListener(action);
    }

    private void UpdateAutoModeTextColor()
    {
        if (autoModeText != null)
        {
            autoModeText.color = isAutoPlaying ? autoColor : defaultColor; // 状態に応じて色を設定
        }
    }

    private void UpdateNextButtonState()
    {
        if (nextButton != null)
        {
            nextButton.interactable = !isAutoPlaying; // オート中は無効化
        }
    }

}
