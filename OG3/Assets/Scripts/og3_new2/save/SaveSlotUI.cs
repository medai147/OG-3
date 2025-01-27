using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public List<GameObject> saveSlots; // スロットのGameObjectを手動でアタッチ
    public GameObject saveConfirmationPopup; // ポップアップUI
    public Text confirmationText; // 確認テキスト
    public Button confirmButton; // 「はい」ボタン
    public Button cancelButton; // 「いいえ」ボタン

    private SaveManager saveManager;
    private StoryManager storyManager;
    private int selectedSlot = -1; // 現在選択されたスロット

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

        // ポップアップの初期化
        if (saveConfirmationPopup != null)
        {
            saveConfirmationPopup.SetActive(false); // 最初は非表示
        }
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1; // スロット番号
            SaveData saveData = saveManager.LoadGame(slot);

            GameObject slotObject = saveSlots[i];
            Text slotText = slotObject.transform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotObject.transform.GetChild(1).GetComponent<Image>();

            if (saveData != null)
            {
                slotText.text = $"名前：{saveData.heroineName}\nセーブ日時: {saveData.saveTimestamp}";
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\nストーリーデータが見つかりません";

                if (File.Exists(saveData.screenshotPath))
                {
                    byte[] fileData = File.ReadAllBytes(saveData.screenshotPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    slotImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
                else
                {
                    slotImage.sprite = null;
                }
            }
            else
            {
                slotText.text = "空スロット";
                slotImage.sprite = null;
            }

            Button slotButton = slotObject.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => ShowSaveConfirmation(slot));
            }
        }
    }

    void ShowSaveConfirmation(int slot)
    {
        selectedSlot = slot; // 選択されたスロットを記録
        confirmationText.text = $"スロット {slot} にセーブしますか？";
        saveConfirmationPopup.SetActive(true); // ポップアップを表示

        // 「はい」ボタンと「いいえ」ボタンのイベントを設定
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(ConfirmSave);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => saveConfirmationPopup.SetActive(false));
    }

    void ConfirmSave()
    {
        if (selectedSlot < 1)
        {
            Debug.LogError("スロットが選択されていません。");
            return;
        }

        // ゲームをセーブ
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager のインスタンスが見つかりません。");
            return;
        }

        GameManager.instance.gameStateManager.SaveState(saveManager, selectedSlot);
        Debug.Log($"スロット {selectedSlot} にゲームをセーブしました。");

        // ポップアップを閉じる
        saveConfirmationPopup.SetActive(false);

        // スロット情報を更新
        DisplayAllSlots();
    }
}
