using UnityEngine;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI管理クラス
    public SoundManager soundManager; // サウンド管理クラス

    private StoryManager storyManager; // ストーリーマネージャ
    private int currentStoryIndex = 1; // 現在のストーリー番号

    public GameStateManager gameStateManager = new GameStateManager();


    void Start()
    {
        // StoryManagerの初期化
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

        // UIManagerの月画像非表示イベントを登録
        uiManager.OnMonthImageHidden += NextStory;

        // 最初のストーリーを表示
        DisplayStory(gameStateManager.CurrentStoryID);

        // UIボタンのクリック処理を登録
        uiManager.SetNextButtonAction(NextStory);
    }

    void DisplayStory(int storyIndex)
    {
        StoryData story = storyManager.GetStory(storyIndex);

        if (story != null)
        {
            // UIとオーディオを更新
            uiManager.UpdateUI(story);
            soundManager.PlayAudio(story.bgm, story.se);
        }
        else
        {
            Debug.LogError("ストーリーデータが見つかりません: " + storyIndex);
        }
    }

    public void NextStory()
    {
        gameStateManager.CurrentStoryID++;
        if (gameStateManager.CurrentStoryID < storyManager.stories.Count)
        {
            DisplayStory(gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("ストーリー終了");
            // 必要に応じてシーン遷移やエンディング処理
        }
    }


    void OnDestroy()
    {
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
