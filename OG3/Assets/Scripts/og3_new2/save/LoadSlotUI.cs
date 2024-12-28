using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlotUI : MonoBehaviour
{
    public Button[] loadSlotButtons;
    public Image screenshotPreview; // �X�N���[���V���b�g��\������UI

    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();

        for (int i = 0; i < loadSlotButtons.Length; i++)
        {
            int slot = i + 1; // �X���b�g�ԍ�
            loadSlotButtons[i].onClick.AddListener(() => OnLoadSlotClicked(slot));
        }
    }

    void OnLoadSlotClicked(int slot)
    {
        SaveData saveData = saveManager.LoadGame(slot);
        if (saveData != null)
        {
            // �X�N���[���V���b�g��\��
            if (File.Exists(saveData.screenshotPath))
            {
                byte[] imageData = File.ReadAllBytes(saveData.screenshotPath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageData);

                screenshotPreview.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogWarning("Screenshot not found at " + saveData.screenshotPath);
            }

            // ���̃Z�[�u�f�[�^���Q�[���ɓK�p
            GameManager.instance.heroinename = saveData.heroineName;
            GameManager.instance.storynum = saveData.currentStoryIndex;
            GameManager.instance.affectionPoints = saveData.affectionPoints;

            Debug.Log("Game data loaded for slot " + slot);
        }
    }
}
