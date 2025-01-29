using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI管理クラス
    public SoundManager soundManager; // サウンド管理クラス
    public AutoPlayManager autoPlayManager; 
    public FadeAnimationManager fadeAnimationManager;
    public MoveManager moveManager; // MoveManagerを参照
    public ScreenAnimationManager screenAnimationManager;
    public CharacterAnimationManager characterAnimationManager;

    private StoryManager storyManager; // ストーリーマネージャ

    public GameStateManager gameStateManager = new GameStateManager();


    private int lastSkippedStoryIndex = -1; // 最後にスキップしたストーリー番号を記録

    void Start()
    {
        GameManager.instance.previousSceneName = SceneManager.GetActiveScene().name; // 現在のシーン名を記録

        // StoryManagerの初期化
        storyManager = new StoryManager();
        string storyCsvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(storyCsvPath);

        // ChoiceManagerの初期化
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (choiceManager != null)
        {
            // 選択肢データのCSVパスを指定
            string choiceCsvPath = Application.dataPath + "/StreamingAssets/SelectCsv.csv";
            choiceManager.LoadChoices(choiceCsvPath);
        }
        else
        {
            Debug.LogError("ChoiceManager が見つかりません。");
        }

        // UIManagerの月画像非表示イベントを登録
        uiManager.OnMonthImageHidden += NextStory;

        uiManager.SetNextButtonAction(NextStory);

        // AutoPlayManager の初期化
        if (autoPlayManager != null)
        {
            autoPlayManager.Initialize(this);
        }
        else
        {
            Debug.LogError("AutoPlayManager が設定されていません。");
        }

        // 最初のストーリーを表示
        DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
    }


    public void DisplayStory(int storyIndex)
    {
        // ストーリーデータを取得
        StoryData story = storyManager.GetStory(storyIndex);

        if (story != null)
        {

            screenAnimationManager.PlayScreenAnimation(story.screenanimation);


            //characteranimationが指定されている場合
            characterAnimationManager.PlayCharacterAnimation(story.characteranimation);


            // フェードアニメーションがない場合は直接UIとオーディオを更新
            UpdateStoryUIAndChoices(story);
            // moveanimation列を確認してMoveManagerを更新
            if (!string.IsNullOrEmpty(story.moveanimation))
            {
                // 140秒後に次のストーリーに進む
                StartCoroutine(WaitAndProceed(2.13f));
                autoPlayManager.StopAutoPlay();
                moveManager.ShowMove(story.moveanimation);

                return; 
            }

            // fadeanimationが指定されている場合
            if (!string.IsNullOrEmpty(story.fadeanimation))
            {
                // フェード開始時にオートモードを停止
                autoPlayManager.StopAutoPlay();

                fadeAnimationManager.PlayFadeAnimation(story.fadeanimation, () =>
                {
                    // フェードアニメーションが完了したらUIとオーディオを更新
                    UpdateStoryUIAndChoices(story);
                    NextStory();
                });
            }

        }
        else
        {
            //終了処理
            SceneManager.LoadScene("FinishScene2");

            Debug.LogError("ストーリーデータが見つかりません: " + storyIndex);
        }
    }

    /// <summary>
    /// 指定秒数待機後に次のストーリーを進める
    /// </summary>
    private IEnumerator WaitAndProceed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // 指定時間待機
        moveManager.HideMove();
        NextStory(); // 次のストーリーに進む
    }

    /// <summary>
    /// UI、オーディオの更新と選択肢表示を行う
    /// </summary>
    /// <param name="story">更新するストーリーデータ</param>
    private void UpdateStoryUIAndChoices(StoryData story)
    {
        // UIとオーディオを更新
        uiManager.UpdateUI(story);
        soundManager.PlayAudio(story.bgm, story.se);

        // 選択肢がある場合は選択肢を表示
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (story.selectid > 0 && choiceManager != null)
        {
            // フェード開始時にオートモードを停止
            autoPlayManager.StopAutoPlay();
            // 過去の選択肢をクリア
            choiceManager.ClearChoicesUI();

            // 現在の選択肢を表示
            choiceManager.DisplayChoicesForStory(story.selectid);
        }
    }




    public void NextStory()
    {
        // 現在のストーリー番号を取得
        int currentStoryIndex = GameManager.instance.gameStateManager.CurrentStoryID;

        // 現在の選択肢の範囲を取得
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (choiceManager != null)
        {
            // 現在の `currentStoryID` がどの `end` に一致するかチェック
            foreach (ChoiceData choice in choiceManager.choices)
            {
                if (currentStoryIndex == choice.EndRange)
                {
                    int selectId = choice.SelectID;

                    // 同じ selectID を持つ `EndRange` の最大値を取得
                    int maxEndRange = choiceManager.GetMaxEndRange(selectId);


                    // スキップ処理
                    GameManager.instance.gameStateManager.CurrentStoryID = maxEndRange + 1;
  
                    Debug.Log($"ストーリー範囲をスキップして {maxEndRange + 1} へ移動します。");

                    DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
                    return;
                }
            }
        }
        // 次のストーリーへ進む
        GameManager.instance.gameStateManager.CurrentStoryID++;
        if (GameManager.instance.gameStateManager.CurrentStoryID <= storyManager.stories.Count)
        {
            DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("ストーリー終了");
            // 必要に応じてエンディング処理やシーン遷移
        }
    }



    void OnDestroy()
    {
        // イベントの解除
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
