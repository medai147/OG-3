using UnityEngine;
using UnityEngine.UI;

public class SettingSystem : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button[] autoSpeedButtons;
    [SerializeField] private Button[] textSpeedButtons;
    [SerializeField] private Button[] seVolumeButtons;
    [SerializeField] private Button[] bgmVolumeButtons;

    public AudioSource seSource;
    public AudioClip seAudioClip;

    private void Start()
    {
        UpdateButtonStates();
    }

    public void SetAutoSpeed(int speed)
    {
        SettingManager.Instance.SetAutoSpeed(speed);
        UpdateButtonStates();
    }

    public void SetTextSpeed(int speed)
    {
        SettingManager.Instance.SetTextSpeed(speed);
        UpdateButtonStates();
    }

    public void SetSEVolume(int volume)
    {
        SettingManager.Instance.SetSEVolume(volume);
        seSource.PlayOneShot(seAudioClip);
        UpdateButtonStates();
    }

    public void SetBGMVolume(int volume)
    {
        SettingManager.Instance.SetBGMVolume(volume);
        UpdateButtonStates();
    }

    public void CloseSettings()
    {
        settingPanel.SetActive(false);
    }

    private void UpdateButtonStates()
    {
        // �������x�{�^���̏�Ԃ��X�V
        for (int i = 0; i < autoSpeedButtons.Length; i++)
        {
            autoSpeedButtons[i].GetComponent<Image>().enabled = (i + 1 == SettingManager.Instance.AutoSpeed);
        }

        // �e�L�X�g���x�{�^���̏�Ԃ��X�V
        for (int i = 0; i < textSpeedButtons.Length; i++)
        {
            textSpeedButtons[i].GetComponent<Image>().enabled = (i + 1 == SettingManager.Instance.TextSpeed);
        }

        // ���ʉ��{�^���̏�Ԃ��X�V
        for (int i = 0; i < seVolumeButtons.Length; i++)
        {
            seVolumeButtons[i].GetComponent<Image>().enabled = (i + 1 == SettingManager.Instance.SEVolume);
        }

        // BGM�{�^���̏�Ԃ��X�V
        for (int i = 0; i < bgmVolumeButtons.Length; i++)
        {
            bgmVolumeButtons[i].GetComponent<Image>().enabled = (i + 1 == SettingManager.Instance.BGMVolume);
        }
    }
}
