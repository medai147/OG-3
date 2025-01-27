using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public List<GameObject> saveSlots; // �X���b�g��GameObject���蓮�ŃA�^�b�`
    public GameObject saveConfirmationPopup; // �|�b�v�A�b�vUI
    public Text confirmationText; // �m�F�e�L�X�g
    public Button confirmButton; // �u�͂��v�{�^��
    public Button cancelButton; // �u�������v�{�^��

    private SaveManager saveManager;
    private StoryManager storyManager;
    private int selectedSlot = -1; // ���ݑI�����ꂽ�X���b�g

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

        // �|�b�v�A�b�v�̏�����
        if (saveConfirmationPopup != null)
        {
            saveConfirmationPopup.SetActive(false); // �ŏ��͔�\��
        }
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1; // �X���b�g�ԍ�
            SaveData saveData = saveManager.LoadGame(slot);

            GameObject slotObject = saveSlots[i];
            Text slotText = slotObject.transform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotObject.transform.GetChild(1).GetComponent<Image>();

            if (saveData != null)
            {
                slotText.text = $"���O�F{saveData.heroineName}\n�Z�[�u����: {saveData.saveTimestamp}";
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\n�X�g�[���[�f�[�^��������܂���";

                if (File.Exists(saveData.screenshotPath))
                {
                    byte[] fileData = File.ReadAllBytes(saveData.screenshotPath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    slotImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                }
                else
                {
                    slotImage.sprite = null;
                }
            }
            else
            {
                slotText.text = "��X���b�g";
                slotImage.sprite = null;
            }

            Button slotButton = slotObject.GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();
                slotButton.onClick.AddListener(() => ShowSaveConfirmation(slot));
            }
        }
    }

    void ShowSaveConfirmation(int slot)
    {
        selectedSlot = slot; // �I�����ꂽ�X���b�g���L�^
        confirmationText.text = $"�X���b�g {slot} �ɃZ�[�u���܂����H";
        saveConfirmationPopup.SetActive(true); // �|�b�v�A�b�v��\��

        // �u�͂��v�{�^���Ɓu�������v�{�^���̃C�x���g��ݒ�
        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(ConfirmSave);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(() => saveConfirmationPopup.SetActive(false));
    }

    void ConfirmSave()
    {
        if (selectedSlot < 1)
        {
            Debug.LogError("�X���b�g���I������Ă��܂���B");
            return;
        }

        // �Q�[�����Z�[�u
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager �̃C���X�^���X��������܂���B");
            return;
        }

        GameManager.instance.gameStateManager.SaveState(saveManager, selectedSlot);
        Debug.Log($"�X���b�g {selectedSlot} �ɃQ�[�����Z�[�u���܂����B");

        // �|�b�v�A�b�v�����
        saveConfirmationPopup.SetActive(false);

        // �X���b�g�����X�V
        DisplayAllSlots();
    }
}
