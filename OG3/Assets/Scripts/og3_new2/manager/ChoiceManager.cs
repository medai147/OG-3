using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//選択肢に関する処理を管理するクラス
//条件付き選択肢の表示や選択結果の処理を担当
public class ChoiceManager
{
    private List<ChoiceData> choices;

    public void LoadChoices(string csvPath)
    {
        // CSVを読み込み、ChoiceDataオブジェクトのリストに変換
    }

    public List<ChoiceData> GetChoicesForSelectID(int selectID)
    {
        // 指定した選択肢IDに対応する選択肢リストを返す
        return choices;
    }
}

public class ChoiceData
{
    public int SelectID;
    public string Text;
    public int NextSceneID;
    public string TargetCharacter;
    public int AffectionChange;
    public string Condition;
}

