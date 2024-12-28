using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string SaveDirectory = "/Saves/";
    private const string SaveFileName = "SaveSlot";

    /// <summary>
    /// �Q�[���̏�Ԃ��w�肵���X���b�g�ɃZ�[�u���܂��B
    /// </summary>
    /// <param name="slot">�Z�[�u�X���b�g�ԍ�</param>
    /// <param name="saveData">�ۑ�����f�[�^</param>
    public void SaveGame(int slot, SaveData saveData)
    {
        string path = Application.persistentDataPath + SaveDirectory;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = Path.Combine(path, SaveFileName + slot + ".json");
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(fullPath, json);

        Debug.Log($"Game saved to slot {slot}: {fullPath}");
    }

    /// <summary>
    /// �w�肵���X���b�g����Q�[���̏�Ԃ����[�h���܂��B
    /// </summary>
    /// <param name="slot">���[�h����X���b�g�ԍ�</param>
    /// <returns>���[�h���ꂽ�f�[�^�B�f�[�^�����݂��Ȃ��ꍇ�� null�B</returns>
    public SaveData LoadGame(int slot)
    {
        string path = Application.persistentDataPath + SaveDirectory + SaveFileName + slot + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Game loaded from slot {slot}: {path}");
            return saveData;
        }
        else
        {
            Debug.LogWarning($"No save data found in slot {slot}: {path}");
            return null;
        }
    }

    /// <summary>
    /// �w�肵���X���b�g�̃Z�[�u�f�[�^���폜���܂��B
    /// </summary>
    /// <param name="slot">�폜����X���b�g�ԍ�</param>
    public void DeleteSave(int slot)
    {
        string path = Application.persistentDataPath + SaveDirectory + SaveFileName + slot + ".json";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"Save data deleted from slot {slot}: {path}");
        }
        else
        {
            Debug.LogWarning($"No save data to delete in slot {slot}: {path}");
        }
    }

    /// <summary>
    /// ���ׂẴZ�[�u�X���b�g���폜���܂��B
    /// </summary>
    public void DeleteAllSaves()
    {
        string path = Application.persistentDataPath + SaveDirectory;

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            Debug.Log($"All save data deleted: {path}");
        }
        else
        {
            Debug.LogWarning($"No save directory found to delete: {path}");
        }
    }
}
