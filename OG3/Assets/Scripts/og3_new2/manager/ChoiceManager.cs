using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    private List<ChoiceData> choices = new List<ChoiceData>();

    [SerializeField] private GameObject choiceButtonPrefab; // 選択肢ボタンのプレハブ
    [SerializeField] private Transform choiceContainer; // 選択肢ボタンを配置するコンテナ
    [SerializeField] private GameObject backImage;

    private ChoiceData currentChoice;

    // CSVデータの読み込み
    public void LoadChoices(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("選択肢CSVファイルが見つかりません: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++) // ヘッダー行をスキップ
        {
            string[] data = lines[i].Split(',');

            try
            {
                ChoiceData choice = new ChoiceData
                {
                    SelectID = int.Parse(data[0]),
                    Text = data[1],
                    NextSceneID = int.Parse(data[2]),
                    EndRange = int.Parse(data[3]), // CSVから終了範囲を取得
                    TargetCharacter = data[4],
                    AffectionChange = int.Parse(data[5]),
                    Condition = string.IsNullOrEmpty(data[6]) ? null : data[6]
                };

                choices.Add(choice);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"選択肢データの解析に失敗しました (行 {i + 1}): {ex.Message}");
            }
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

        //フェード実験のためにストーリー番号がずれたから+2している後で修正
        storySystem.DisplayStory(choice.NextSceneID + 2);
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
        int maxEndRange = -1;

        foreach (ChoiceData choice in choices)
        {
            if (choice.SelectID == selectID)
            {
                if (choice.EndRange > maxEndRange)
                {
                    maxEndRange = choice.EndRange;
                }
            }
        }

        return maxEndRange;
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
