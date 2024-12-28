using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    public int AutoSpeed { get; private set; } = 2; // デフォルト: 中速
    public int TextSpeed { get; private set; } = 2; // デフォルト: 中速
    public int SEVolume { get; private set; } = 2; 
    public int BGMVolume { get; private set; } = 2; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAutoSpeed(int speed) => AutoSpeed = Mathf.Clamp(speed, 1, 3);
    public void SetTextSpeed(int speed) => TextSpeed = Mathf.Clamp(speed, 1, 3);
    public void SetSEVolume(int volume) => SEVolume = Mathf.Clamp(volume, 1, 3);
    public void SetBGMVolume(int volume) => BGMVolume = Mathf.Clamp(volume, 1, 3);
}
