using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateManager
{
    public string heroineName = "はる";
    public int CurrentStoryID { get; set; } = 1; // 初期ストーリーID 1スタート
    public Dictionary<string, int> AffectionPoints { get; private set; } = new Dictionary<string, int>();

    public List<string> loglist { get; set; } = new List<string>();

    // 各月の最終ストーリー番号を定義
    private Dictionary<string, int> storyEndByMonth = new Dictionary<string, int>
    {
        { "4月", 50 }, 
        { "5月", 140 },
        { "6月", 190 }, 
        { "7月", 259 }, 
        { "8月", 333 }, 
        { "9月", 396 },
        // 必要に応じて追加
    };

    // 現在のストーリー番号から対応する月を取得
    public string GetMonth(int storynum)
    {
        foreach (var entry in storyEndByMonth)
        {
            if (storynum <= entry.Value)
            {
                return entry.Key; // ストーリー番号が範囲内なら対応する月を返す
            }
        }
        return "不明"; // 該当する月がない場合
    }
    
    public GameStateManager()
    {
        // キャラクターごとの好感度を初期化
        AffectionPoints["Hikaru"] = 0;
        AffectionPoints["Ouji"] = 0;
        AffectionPoints["Rukia"] = 0;
    }

    public void SaveState(SaveManager saveManager, int slot)
    {
        Debug.Log("Saving loglist count: " + loglist.Count);
        SaveData saveData = new SaveData
        {
            heroineName = heroineName,
            currentStoryIndex = CurrentStoryID,
            affectionPoints = AffectionPoints,
            screenshotPath = GameManager.instance.screenshotpath,
            loglist = loglist
        };
        saveManager.SaveGame(slot, saveData);
    }

    public void LoadState(SaveManager saveManager, int slot)
    {
        SaveData saveData = saveManager.LoadGame(slot);
        if (saveData != null)
        {
            CurrentStoryID = saveData.currentStoryIndex;
            AffectionPoints = saveData.affectionPoints;
        }
    }
}
