using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string heroineName;
    public int currentStoryIndex;
    public Dictionary<string, int> affectionPoints; // 好感度データ
    public string screenshotPath; // スクリーンショットのパス
}
