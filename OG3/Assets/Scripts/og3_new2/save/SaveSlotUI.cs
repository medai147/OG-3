using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public List<GameObject> saveSlots; // スロットのGameObjectを手動でアタッチ

    private SaveManager saveManager;
    private StoryManager storyManager;

    void Start()
    {
        // SaveManagerがシーン内に存在するか確認
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager が見つかりません。シーンに SaveManager を配置してください。");
            return;
        }

        // StoryManagerがシーン内に存在するか確認
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

        // 各スロットを表示
        DisplayAllSlots();
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1; // スロット番号
            SaveData saveData = saveManager.LoadGame(slot);

            // スロットのテキストと画像を取得
            GameObject slotObject = saveSlots[i];
            Text slotText = slotObject.transform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotObject.transform.GetChild(1).GetComponent<Image>();

            if (saveData != null)
            {
                // 名前とストーリー番号を表示
                slotText.text = $"名前：{saveData.heroineName}";
                // セーブ日時を表示
                slotText.text += $"\nセーブ日時: {saveData.saveTimestamp}";
                // ストーリー番号からメインテキストを取得して追加表示
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\nストーリーデータが見つかりません";



                // 画像を表示
                if (File.Exists(saveData.screenshotPath))
                {
                    byte[] fileData = File.ReadAllBytes(saveData.screenshotPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    slotImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
                else
                {
                    slotImage.sprite = null; // デフォルト画像
                }
            }
            else
            {
                // セーブデータがない場合は空表示
                slotText.text = "空スロット";
                slotImage.sprite = null; // デフォルト画像
            }

            // ボタンのクリックイベントを登録
            Button slotButton = slotObject.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => OnSaveSlotClicked(slot));
            }
        }
    }

    void OnSaveSlotClicked(int slot)
    {
        // GameManagerにアクセスして、GameStateManagerの状態をセーブ
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager のインスタンスが見つかりません。");
            return;
        }

        // GameStateManagerを取得し、現在の状態をセーブ
        GameManager.instance.gameStateManager.SaveState(saveManager, slot);

        Debug.Log($"スロット {slot} にゲームをセーブしました。");

        // 再度スロット情報を更新
        DisplayAllSlots();
    }
}
