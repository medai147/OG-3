using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI管理クラス
    public SoundManager soundManager; // サウンド管理クラス

    private StoryManager storyManager; // ストーリーマネージャ

    public GameStateManager gameStateManager = new GameStateManager();


    void Start()
    {
        GameManager.instance.previousSceneName = SceneManager.GetActiveScene().name; // 現在のシーン名を記録

        // StoryManagerの初期化
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

        // UIManagerの月画像非表示イベントを登録
        uiManager.OnMonthImageHidden += NextStory;

        uiManager.SetNextButtonAction(NextStory);

        // 最初のストーリーを表示
        DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
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
        GameManager.instance.gameStateManager.CurrentStoryID++;
        if (GameManager.instance.gameStateManager.CurrentStoryID < storyManager.stories.Count)
        {
            DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("ストーリー終了");
            // 必要に応じてシーン遷移やエンディング処理
        }
    }


    void OnDestroy()
    {
        // イベントの解除
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
