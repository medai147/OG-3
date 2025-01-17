using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private GameObject moveImage; // 場所表示用のオブジェクト
    [SerializeField] private Text moveText; // moveImage内のTextコンポーネント

    /// <summary>
    /// 場所名を設定して表示
    /// </summary>
    /// <param name="locationName">表示する場所名</param>
    public void ShowMove(string locationName)
    {
        if (!string.IsNullOrEmpty(locationName))
        {
            moveText.text = locationName; // テキストを設定
            moveImage.SetActive(true);   // 表示
        }
        else
        {
            moveImage.SetActive(false); // 非表示
        }
    }

    /// <summary>
    /// 強制的に非表示にする
    /// </summary>
    public void HideMove()
    {
        if (moveImage != null)
        {
            moveImage.SetActive(false);
        }
        
    }
}
