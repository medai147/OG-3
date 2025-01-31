using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public List<ChoiceData> choices = new List<ChoiceData>();

    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private Transform choiceContainer;
    [SerializeField] private GameObject backImage;

    private ChoiceData currentChoice;

    public void LoadChoices(string csvFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        if (csvData == null)
        {
            Debug.LogError("指定されたCSVファイルが見つかりません: " + csvFileName);
            return;
        }

        string[] lines = csvData.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // ヘッダー行をスキップ
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] data = line.Split(',');

            if (data.Length < 7) // カラムが足りているか確認
            {
                Debug.LogError($"データ列が不足しています (行 {i + 1}): {line}");
                continue;
            }

            try
            {
                ChoiceData choice = new ChoiceData
                {
                    SelectID = TryParseInt(data[0], i + 1, "SelectID"),
                    Text = data[1],
                    NextSceneID = TryParseInt(data[2], i + 1, "NextSceneID"),
                    EndRange = TryParseInt(data[3], i + 1, "EndRange"),
                    TargetCharacter = data[4],
                    AffectionChange = TryParseInt(data[5], i + 1, "AffectionChange"),
                    Condition = string.IsNullOrEmpty(data[6]) ? null : data[6]
                };

                choices.Add(choice);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"選択肢データの解析に失敗しました (行 {i + 1}): {line} - {ex.Message}");
            }
        }

        Debug.Log($"選択肢データを読み込みました: {choices.Count}件");
    }

    private int TryParseInt(string value, int lineNumber, string fieldName)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogError($"数値変換エラー: {fieldName} (行 {lineNumber}): \"{value}\"");
            return 0; // デフォルト値を返す
        }
    }



// 指定したストーリーの選択肢を表示する
public void DisplayChoicesForStory(int storySelectID)
    {
        if (storySelectID <= 0)
        {
            Debug.Log("選択肢IDが0または無効のため、選択肢は表示されません。");
            return;
        }

        // 該当する選択肢を取得
        List<ChoiceData> availableChoices = GetChoicesForSelectID(storySelectID);

        if (availableChoices == null || availableChoices.Count == 0)
        {
            Debug.LogWarning($"選択肢ID {storySelectID} に対応する選択肢が見つかりません。");
            return;
        }

        backImage.SetActive(true);
        // 選択肢をUIに表示
        float buttonSpacing = 80f; // ボタン間のスペース
        float startY = 30f; // ボタンの初期Y位置
        int index = 0; // ボタンのインデックス

        foreach (ChoiceData choice in availableChoices)
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);

            // ボタンテキストを設定
            Text buttonText = choiceButton.GetComponentInChildren<Text>();
            buttonText.text = choice.Text;

            // ボタンの位置を調整
            RectTransform rectTransform = choiceButton.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(0, startY - (buttonSpacing * index));
            }

            // ボタンのクリックイベントを設定
            choiceButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnChoiceSelected(choice);
            });

            index++;
        }
    }


    // 指定した選択肢IDに対応する選択肢リストを返す
    public List<ChoiceData> GetChoicesForSelectID(int selectID)
    {
        List<ChoiceData> validChoices = new List<ChoiceData>();

        foreach (ChoiceData choice in choices)
        {
            if (choice.SelectID == selectID)
            {
                if (string.IsNullOrEmpty(choice.Condition) || EvaluateCondition(choice.Condition))
                {
                    validChoices.Add(choice);
                }
            }
        }

        return validChoices;
    }

    // 条件を評価するメソッド
    private bool EvaluateCondition(string condition)
    {
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();

        if (characterManager != null && condition.Contains(">"))
        {
            string[] parts = condition.Split('>');
            string character = parts[0];
            int threshold = int.Parse(parts[1]);

            return characterManager.GetAffection(character) > threshold;
        }

        return true; // デフォルトで条件を満たすとする
    }

    // 選択肢が選ばれた時の処理
    public void OnChoiceSelected(ChoiceData choice)
    {
        currentChoice = choice; // 現在の選択肢を記録

        if (!string.IsNullOrEmpty(choice.TargetCharacter))
        {
            CharacterManager characterManager = FindObjectOfType<CharacterManager>();
            characterManager.UpdateAffection(choice.TargetCharacter, choice.AffectionChange);
        }

        // 背景画像を非表示
        backImage.SetActive(false);

        // 選択肢UIをクリア
        ClearChoicesUI();

        // 選択肢のNextSceneIDにジャンプ
        GameManager.instance.gameStateManager.CurrentStoryID = choice.NextSceneID;
        StorySystem storySystem = FindObjectOfType<StorySystem>();

        storySystem.DisplayStory(choice.NextSceneID);
    }


    public void ClearChoicesUI()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject); // 子オブジェクト（選択肢ボタン）を削除
        }
    }

    public int GetMaxEndRange(int selectID)
    {
        return choices.Where(c => c.SelectID == selectID).Max(c => c.EndRange);
    }

    public ChoiceData GetCurrentChoice()
    {
        return currentChoice;
    }



}

// 選択肢データを保持するクラス
public class ChoiceData
{
    public int SelectID;
    public string Text;
    public int NextSceneID;
    public string TargetCharacter;
    public int AffectionChange;
    public string Condition;
    public int EndRange; // 終了番号
}
