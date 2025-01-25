using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logPanel; // ログパネルの参照
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// セーブシーンに遷移
    /// </summary>
    public void GoToSaveScene()
    {
        // 必要に応じて現在の状態を保存する処理を追加可能
        SceneManager.LoadScene("savescene_new2"); // セーブシーンに遷移
    }

    /// <summary>
    /// ロードシーンに遷移
    /// </summary>
    public void GoToLoadScene()
    {
        SceneManager.LoadScene("loadscene"); // ロードシーンに遷移
    }

    /// <summary>
    /// タイトルシーンに遷移
    /// </summary>
    public void GoToTitleScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("startscene_new2"); // ロードシーンに遷移
    }

    /// <summary>
    /// ログパネルの表示/非表示を切り替える
    /// </summary>
    public void ToggleLogPanel()
    {
        if (logPanel != null)
        {
            logPanel.SetActive(!logPanel.activeSelf); // 表示/非表示を切り替え
        }
        else
        {
            Debug.LogError("ログパネルが設定されていません！");
        }
    }

    /// <summary>
    /// メニュー画面を閉じる
    /// </summary>
    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        menuPanel.SetActive(false); // 現在のメニューを非表示
    }
}
