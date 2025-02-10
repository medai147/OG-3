using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager が見つかりません。シーンに SaveManager を配置してください。");
            return;
        }

        storyManager = new StoryManager();

        // Resources フォルダから StoryCsv.csv を読み込む
        storyManager.LoadStories("StoryCsv");
        DisplayAllSlots();

        if (saveConfirmationPopup != null)
        {
            saveConfirmationPopup.SetActive(false);
        }
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1;
            SaveData saveData = saveManager.LoadGame(slot);

            GameObject slotObject = saveSlots[i];
            Text slotText = slotObject.transform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotObject.transform.GetChild(1).GetComponent<Image>();

            if (saveData != null)
            {
                slotText.text = $"名前：{saveData.heroineName}\nセーブ日時: {saveData.saveTimestamp}";
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\nストーリーデータが見つかりません";

                LoadScreenshot(saveData.screenshotPath, slotImage);
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

    void LoadScreenshot(string filePath, Image targetImage)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            targetImage.sprite = null;
            return;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);
        targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    void ShowSaveConfirmation(int slot)
    {
        selectedSlot = slot;
        confirmationText.text = $"スロット {slot} にセーブしますか？";
        saveConfirmationPopup.SetActive(true);

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

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager のインスタンスが見つかりません。");
            return;
        }

        GameManager.instance.gameStateManager.SaveState(saveManager, selectedSlot);
        Debug.Log($"スロット {selectedSlot} にゲームをセーブしました。");

        saveConfirmationPopup.SetActive(false);
        DisplayAllSlots();
    }
}
