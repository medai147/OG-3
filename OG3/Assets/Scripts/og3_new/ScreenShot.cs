using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [Header("保存先の設定")]
    [SerializeField] string folderName = "Screenshots";

    bool isCreatingScreenShot = false;
    string path;

    void Start()
    {
        // WebGL では Application.persistentDataPath を使う
        path = Path.Combine(Application.persistentDataPath, folderName);

        // フォルダが存在しなければ作成
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public void PrintScreen()
    {
        StartCoroutine(PrintScreenInternal());
    }

    IEnumerator PrintScreenInternal()
    {
        if (isCreatingScreenShot)
        {
            yield break;
        }

        isCreatingScreenShot = true;

        yield return new WaitForEndOfFrame();

        // スクリーンショットを撮影
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        // ファイル名を生成
        string date = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
        string fileName = Path.Combine(path, date + ".png");

        // 画像データをPNGに変換
        byte[] imageData = screenTexture.EncodeToPNG();
        Destroy(screenTexture); // メモリを解放

        // WebGL の場合、ローカルストレージに保存
        File.WriteAllBytes(fileName, imageData);

        // GameManager にパスを保存
        GameManager.instance.screenshotpath = fileName;

        isCreatingScreenShot = false;
    }
}
