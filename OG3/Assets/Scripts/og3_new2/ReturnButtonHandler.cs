using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonHandler : MonoBehaviour
{
    public void onclicked_returnbutton()
    {
        string previousScene = GameManager.instance.previousSceneName;

        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene); // 記録されたシーン名に戻る
            GameManager.instance.previousSceneName = "";
        }
        else
        {
            Debug.LogWarning("前のシーン名が記録されていません。デフォルトのシーンに戻ります。");
            SceneManager.LoadScene("StartScene"); // デフォルトの戻り先
        }
    }
}
