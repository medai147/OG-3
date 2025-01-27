using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logPanel; // ログパネルの参照
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Scrollbar scrollbar; // Scrollbarコンポーネント

    bool deleteflag = false;
    GameObject[] deletes;

    /// <summary>
    /// セーブシーンに遷移
    /// </summary>
    public void GoToSaveScene()
    {
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
            scrollbar.value = 0.0f;

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
        if(!logPanel.activeSelf)
        {
            Time.timeScale = 1.0f;
            menuPanel.SetActive(false); // 現在のメニューを非表示
        }

    }

    public void onClicked_deletebutton()
    {
        deleteflag = true;
        deletes = GameObject.FindGameObjectsWithTag("delete");
        for (int i = 0; i < deletes.Length; i++)
        {
            deletes[i].SetActive(false);
        }
    }

    private void Update()
    {
        // 右クリックで削除したオブジェクトを戻す処理
        if (deleteflag && Input.GetMouseButtonDown(0)){
            deleteflag = false;
            for (int i = 0; i < deletes.Length; i++)
            {
                deletes[i].SetActive(true);
            }
        }


    }
}
