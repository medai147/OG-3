using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateManager
{
    public string heroineName = "�͂�";
    public int CurrentStoryID { get; set; } = 1; // �����X�g�[���[ID 1�X�^�[�g
    public Dictionary<string, int> AffectionPoints { get; private set; } = new Dictionary<string, int>();

    public List<string> loglist { get; set; } = new List<string>();

    // �e���̍ŏI�X�g�[���[�ԍ����`
    private Dictionary<string, int> storyEndByMonth = new Dictionary<string, int>
    {
        { "4��", 50 }, 
        { "5��", 140 },
        { "6��", 190 }, 
        { "7��", 259 }, 
        { "8��", 333 }, 
        { "9��", 396 },
        // �K�v�ɉ����Ēǉ�
    };

    // ���݂̃X�g�[���[�ԍ�����Ή����錎���擾
    public string GetMonth(int storynum)
    {
        foreach (var entry in storyEndByMonth)
        {
            if (storynum <= entry.Value)
            {
                return entry.Key; // �X�g�[���[�ԍ����͈͓��Ȃ�Ή����錎��Ԃ�
            }
        }
        return "�s��"; // �Y�����錎���Ȃ��ꍇ
    }
    
    public GameStateManager()
    {
        // �L�����N�^�[���Ƃ̍D���x��������
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
