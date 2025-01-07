using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateManager
{
    public string heroineName = "�͂�";
    public int CurrentStoryID { get; set; } = 55; // �����X�g�[���[ID
    public Dictionary<string, int> AffectionPoints { get; private set; } = new Dictionary<string, int>();

    public GameStateManager()
    {
        // �L�����N�^�[���Ƃ̍D���x��������
        AffectionPoints["Hikaru"] = 0;
        AffectionPoints["Ouji"] = 0;
        AffectionPoints["Rukia"] = 0;
    }

    public void SaveState(SaveManager saveManager, int slot)
    {
        SaveData saveData = new SaveData
        {
            heroineName = heroineName,
            currentStoryIndex = CurrentStoryID,
            affectionPoints = AffectionPoints,
            screenshotPath = GameManager.instance.screenshotpath
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
