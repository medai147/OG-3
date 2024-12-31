using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string heroineName; // ヒロイン名
    public int currentStoryIndex; // 現在のストーリー番号
    public Dictionary<string, int> affectionPoints; // 好感度データ
    public string screenshotPath; // スクリーンショットのパス
    public string saveTimestamp; // セーブ日時 (ISO8601形式)
}
