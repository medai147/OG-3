using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//キャラクターの状態やアニメーションを管理
//キャラクターの好感度やアニメーションフラグを制御
public class CharacterManager
{
    private Dictionary<string, int> affectionPoints;

    public void InitializeCharacters(List<string> characters)
    {
        foreach (string character in characters)
        {
            affectionPoints[character] = 0; // 初期好感度を設定
        }
    }

    public int GetAffection(string character)
    {
        return affectionPoints.ContainsKey(character) ? affectionPoints[character] : 0;
    }

    public void UpdateAffection(string character, int change)
    {
        if (affectionPoints.ContainsKey(character))
        {
            affectionPoints[character] += change;
        }
    }
}

