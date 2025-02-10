using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [Header("�ۑ���̐ݒ�")]
    [SerializeField] string folderName = "Screenshots";

    bool isCreatingScreenShot = false;
    string path;

    void Start()
    {
        // WebGL �ł� Application.persistentDataPath ���g��
        path = Path.Combine(Application.persistentDataPath, folderName);

        // �t�H���_�����݂��Ȃ���΍쐬
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

        // �X�N���[���V���b�g���B�e
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenTexture.Apply();

        // �t�@�C�����𐶐�
        string date = DateTime.Now.ToString("yy-MM-dd_HH-mm-ss");
        string fileName = Path.Combine(path, date + ".png");

        // �摜�f�[�^��PNG�ɕϊ�
        byte[] imageData = screenTexture.EncodeToPNG();
        Destroy(screenTexture); // �����������

        // WebGL �̏ꍇ�A���[�J���X�g���[�W�ɕۑ�
        File.WriteAllBytes(fileName, imageData);

        // GameManager �Ƀp�X��ۑ�
        GameManager.instance.screenshotpath = fileName;

        isCreatingScreenShot = false;
    }
}
