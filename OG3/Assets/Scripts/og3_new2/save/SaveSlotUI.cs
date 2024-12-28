using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public Button[] saveSlotButtons;

    private SaveManager saveManager;

    void Start()
    {
        // SaveManagerがシーン内に存在するか確認
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager が見つかりません。シーンに SaveManager を配置してください。");
            return;
        }

        // 各スロットボタンにイベントを登録
        for (int i = 0; i < saveSlotButtons.Length; i++)
        {
            int slot = i + 1; // スロット番号
            saveSlotButtons[i].onClick.AddListener(() => OnSaveSlotClicked(slot));
        }
    }

    void OnSaveSlotClicked(int slot)
    {
        // GameManagerが正しく設定されているか確認
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager のインスタンスが見つかりません。");
            return;
        }

        // 必要なデータを取得して保存
        SaveData saveData = new SaveData
        {
            heroineName = GameManager.instance.heroinename,
            currentStoryIndex = GameManager.instance.storynum,
            affectionPoints = GameManager.instance.affectionPoints,
            screenshotPath = GameManager.instance.screenshotpath // 既存のスクリーンショットパス
        };

        saveManager.SaveGame(slot, saveData);

        Debug.Log($"スロット {slot} にゲームをセーブしました。");
    }
}
