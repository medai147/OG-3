using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM 再生用
    public AudioSource seSource; // 効果音再生用

    private void Update()
    {
        // 設定に応じて音量を更新
        bgmSource.volume = SettingManager.Instance.BGMVolume / 3.0f;
        seSource.volume = SettingManager.Instance.SEVolume / 3.0f;
    }

    public void PlayAudio(string bgmName, string seName)
    {
        // BGM 再生
        if (!string.IsNullOrEmpty(bgmName))
        {
            AudioClip bgmClip = Resources.Load<AudioClip>("AudioClips/" + bgmName);
            if (bgmClip != null && bgmSource.clip != bgmClip)
            {
                bgmSource.clip = bgmClip;
                bgmSource.Play();
            }
            if(bgmName.Equals("0"))
            {
                bgmSource.clip = null;
                bgmSource.Stop();
            }
        }

        // 効果音再生
        if (!string.IsNullOrEmpty(seName))
        {
            AudioClip seClip = Resources.Load<AudioClip>("AudioClips/" + seName);
            if (seClip != null)
            {
                seSource.PlayOneShot(seClip);
            }
        }
    }
}
