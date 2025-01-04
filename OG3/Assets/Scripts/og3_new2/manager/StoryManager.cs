using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ストーリー全体の管理を行うクラス
public class StoryManager
{
    public List<StoryData> stories = new List<StoryData>();

    // CSVデータの読み込み
    public void LoadStories(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("指定されたCSVファイルが見つかりません: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++) // ヘッダー行をスキップ
        {
            string line = lines[i];
            string[] data = line.Split(',');

            try
            {
                StoryData story = new StoryData
                {
                    number = int.Parse(data[0]),
                    mainstory = data[1],
                    name = data[2],
                    centercharacterimage = data[3],
                    rightcharacterimage = data[4],
                    leftcharacterimage = data[5],
                    backimage = data[6],
                    stillimage = data[7],
                    characteranimation = data[8],
                    bgm = data[9],
                    se = data[10],
                    selectid = int.Parse(data[11]),
                    monthimage = data[12],
                    fadeanimation = data[13],
                    moveanimation = data[14]
                };

                stories.Add(story);
            }
            catch (System.Exception ex)
            {
               // Debug.LogError($"データの解析に失敗しました (行 {i + 1}): {line} - {ex.Message}");
            }
        }

        Debug.Log($"ストーリーデータを読み込みました: {stories.Count}件");
    }

    // ストーリーIDで検索
    public StoryData GetStory(int id)
    {
        foreach (StoryData story in stories)
        {
            if (story.number == id)
                return story;
        }
        Debug.LogWarning($"指定されたIDのストーリーが見つかりません: {id}");
        return null;
    }
}

// ストーリーデータを保持するクラス
public class StoryData
{
    public int number;
    public string mainstory;
    public string name;
    public string centercharacterimage;
    public string rightcharacterimage;
    public string leftcharacterimage;  
    public string backimage;
    public string stillimage;
    public string characteranimation;
    public string bgm;
    public string se;
    public int selectid;
    public string monthimage;
    public string fadeanimation;
    public string moveanimation;
}
