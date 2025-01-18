using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource; // BGM �Đ��p
    public AudioSource seSource; // ���ʉ��Đ��p

    private void Update()
    {
        // �ݒ�ɉ����ĉ��ʂ��X�V
        bgmSource.volume = SettingManager.Instance.BGMVolume / 3.0f;
        seSource.volume = SettingManager.Instance.SEVolume / 3.0f;
    }

    public void PlayAudio(string bgmName, string seName)
    {
        // BGM �Đ�
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

        // ���ʉ��Đ�
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
