using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject logPanel;

    [SerializeField] private float menuOpenDelay = 0.2f; // ���j���[���J���x�����ԁi�b�j
    private bool isMenuOpening = false;

    public void ToggleMenu()
    {
        if (isMenuOpening)
            return; // ���j���[���J���r���̏ꍇ�͏������Ȃ�

        bool isActive = menuPanel.activeSelf;

        if (!isActive)
        {
            StartCoroutine(OpenMenuWithDelay());
        }
        else
        {
            CloseMenu();
        }
    }

    private IEnumerator OpenMenuWithDelay()
    {
        isMenuOpening = true;


        // �w�莞�ԑҋ@
        yield return new WaitForSeconds(menuOpenDelay);

        // ���j���[���J��
        menuPanel.SetActive(true);
        Time.timeScale = 0; // ���j���[�\�����̓Q�[�����~

        isMenuOpening = false;
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1; // ���j���[�������Q�[�����ĊJ
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }

    public void OpenLog()
    {
        logPanel.SetActive(true);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("startscene_new");
    }
}
