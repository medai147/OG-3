using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject logPanel;

    [SerializeField] private float menuOpenDelay = 0.2f; // メニューを開く遅延時間（秒）
    private bool isMenuOpening = false;

    public void ToggleMenu()
    {
        if (isMenuOpening)
            return; // メニューが開く途中の場合は処理しない

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


        // 指定時間待機
        yield return new WaitForSeconds(menuOpenDelay);

        // メニューを開く
        menuPanel.SetActive(true);
        Time.timeScale = 0; // メニュー表示中はゲームを停止

        isMenuOpening = false;
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1; // メニューを閉じたらゲームを再開
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
