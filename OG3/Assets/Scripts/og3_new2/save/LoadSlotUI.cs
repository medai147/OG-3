using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSlotUI : MonoBehaviour
{
    private SaveManager saveManager;
    private StoryManager storyManager;

    public List<GameObject> saveSlots; // スロットのGameObjectを手動でアタッチ

    void Start()
    {
        // SaveManagerがシーン内に存在するか確認
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager が見つかりません。シーンに SaveManager を配置してください。");
            return;
        }

        // StoryManagerを初期化
        storyManager = new StoryManager();
        storyManager.LoadStories("StoryCsv");

        // 各スロットのデータを表示
        DisplayAllSlots();
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1;
            SaveData saveData = saveManager.LoadGame(slot);

            // スロットの子オブジェクトからコンポーネントを取得
            Transform slotTransform = saveSlots[i].transform;
            Text slotText = slotTransform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotTransform.GetChild(1).GetComponent<Image>();
            Button slotButton = saveSlots[i].GetComponent<Button>();

            if (saveData != null)
            {
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
                    slotImage.sprite = null; // 画像がない場合はデフォルト
                }

                // 名前、ストーリー番号、日時を表示
                slotText.text = $"{saveData.heroineName}\nセーブ日時: {saveData.saveTimestamp}";

                // ストーリー番号からメインテキストを取得して表示
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\nストーリーデータが見つかりません";

                // ロードボタンにイベントを登録
                int currentSlot = slot; // ローカル変数をキャプチャするためにコピー
                slotButton.onClick.AddListener(() => OnLoadSlotClicked(currentSlot));
            }
            else
            {
                // セーブデータがない場合は初期値を設定
                slotImage.sprite = null;
                slotText.text = "空スロット";
            }
        }
    }

    void OnLoadSlotClicked(int slot)
    {
        // SaveManagerからセーブデータをロード
        SaveData saveData = saveManager.LoadGame(slot);
        if (saveData == null)
        {
            Debug.LogWarning($"スロット {slot} にセーブデータが存在しません。");
            return;
        }

        // GameManagerに状態を反映
        GameManager.instance.gameStateManager.CurrentStoryID = saveData.currentStoryIndex;
        GameManager.instance.gameStateManager.heroineName = saveData.heroineName;
        GameManager.instance.gameStateManager.loglist = saveData.loglist;

        Debug.Log($"スロット {slot} のデータをロードしました。");

        // ストーリーシーンへ遷移
        SceneManager.LoadScene("Mainscene_new2");
    }

    public void onclicked_returnbutton()
    {
        SceneManager.LoadScene("StartScene");
    }



}
