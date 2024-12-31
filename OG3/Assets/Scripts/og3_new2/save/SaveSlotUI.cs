using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public List<GameObject> saveSlots; // �X���b�g��GameObject���蓮�ŃA�^�b�`

    private SaveManager saveManager;
    private StoryManager storyManager;

    void Start()
    {
        // SaveManager���V�[�����ɑ��݂��邩�m�F
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager ��������܂���B�V�[���� SaveManager ��z�u���Ă��������B");
            return;
        }

        // StoryManager���V�[�����ɑ��݂��邩�m�F
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

        // �e�X���b�g��\��
        DisplayAllSlots();
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1; // �X���b�g�ԍ�
            SaveData saveData = saveManager.LoadGame(slot);

            // �X���b�g�̃e�L�X�g�Ɖ摜���擾
            GameObject slotObject = saveSlots[i];
            Text slotText = slotObject.transform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotObject.transform.GetChild(1).GetComponent<Image>();

            if (saveData != null)
            {
                // ���O�ƃX�g�[���[�ԍ���\��
                slotText.text = $"���O�F{saveData.heroineName}";
                // �Z�[�u������\��
                slotText.text += $"\n�Z�[�u����: {saveData.saveTimestamp}";
                // �X�g�[���[�ԍ����烁�C���e�L�X�g���擾���Ēǉ��\��
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\n�X�g�[���[�f�[�^��������܂���";



                // �摜��\��
                if (File.Exists(saveData.screenshotPath))
                {
                    byte[] fileData = File.ReadAllBytes(saveData.screenshotPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    slotImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
                else
                {
                    slotImage.sprite = null; // �f�t�H���g�摜
                }
            }
            else
            {
                // �Z�[�u�f�[�^���Ȃ��ꍇ�͋�\��
                slotText.text = "��X���b�g";
                slotImage.sprite = null; // �f�t�H���g�摜
            }

            // �{�^���̃N���b�N�C�x���g��o�^
            Button slotButton = slotObject.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => OnSaveSlotClicked(slot));
            }
        }
    }

    void OnSaveSlotClicked(int slot)
    {
        // GameManager�ɃA�N�Z�X���āAGameStateManager�̏�Ԃ��Z�[�u
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager �̃C���X�^���X��������܂���B");
            return;
        }

        // GameStateManager���擾���A���݂̏�Ԃ��Z�[�u
        GameManager.instance.gameStateManager.SaveState(saveManager, slot);

        Debug.Log($"�X���b�g {slot} �ɃQ�[�����Z�[�u���܂����B");

        // �ēx�X���b�g�����X�V
        DisplayAllSlots();
    }
}
