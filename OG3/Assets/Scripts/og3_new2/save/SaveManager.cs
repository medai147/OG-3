using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private const string SaveDirectory = "/Saves/";
    private const string SaveFileName = "SaveSlot";

    /// <summary>
    /// ゲームの状態を指定したスロットにセーブします。
    /// </summary>
    /// <param name="slot">セーブスロット番号</param>
    /// <param name="saveData">保存するデータ</param>
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
    /// 指定したスロットからゲームの状態をロードします。
    /// </summary>
    /// <param name="slot">ロードするスロット番号</param>
    /// <returns>ロードされたデータ。データが存在しない場合は null。</returns>
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
    /// 指定したスロットのセーブデータを削除します。
    /// </summary>
    /// <param name="slot">削除するスロット番号</param>
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
    /// すべてのセーブスロットを削除します。
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
