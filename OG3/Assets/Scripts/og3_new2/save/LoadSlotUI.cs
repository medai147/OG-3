using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSlotUI : MonoBehaviour
{
    private SaveManager saveManager;
    private StoryManager storyManager;

    public List<GameObject> saveSlots; // �X���b�g��GameObject���蓮�ŃA�^�b�`

    void Start()
    {
        // SaveManager���V�[�����ɑ��݂��邩�m�F
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager ��������܂���B�V�[���� SaveManager ��z�u���Ă��������B");
            return;
        }

        // StoryManager��������
        storyManager = new StoryManager();
        storyManager.LoadStories("StoryCsv");

        // �e�X���b�g�̃f�[�^��\��
        DisplayAllSlots();
    }

    void DisplayAllSlots()
    {
        for (int i = 0; i < saveSlots.Count; i++)
        {
            int slot = i + 1;
            SaveData saveData = saveManager.LoadGame(slot);

            // �X���b�g�̎q�I�u�W�F�N�g����R���|�[�l���g���擾
            Transform slotTransform = saveSlots[i].transform;
            Text slotText = slotTransform.GetChild(0).GetComponent<Text>();
            Image slotImage = slotTransform.GetChild(1).GetComponent<Image>();
            Button slotButton = saveSlots[i].GetComponent<Button>();

            if (saveData != null)
            {
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
                    slotImage.sprite = null; // �摜���Ȃ��ꍇ�̓f�t�H���g
                }

                // ���O�A�X�g�[���[�ԍ��A������\��
                slotText.text = $"{saveData.heroineName}\n�Z�[�u����: {saveData.saveTimestamp}";

                // �X�g�[���[�ԍ����烁�C���e�L�X�g���擾���ĕ\��
                StoryData storyData = storyManager.GetStory(saveData.currentStoryIndex);
                slotText.text += storyData != null ? $"\n{storyData.mainstory}" : "\n�X�g�[���[�f�[�^��������܂���";

                // ���[�h�{�^���ɃC�x���g��o�^
                int currentSlot = slot; // ���[�J���ϐ����L���v�`�����邽�߂ɃR�s�[
                slotButton.onClick.AddListener(() => OnLoadSlotClicked(currentSlot));
            }
            else
            {
                // �Z�[�u�f�[�^���Ȃ��ꍇ�͏����l��ݒ�
                slotImage.sprite = null;
                slotText.text = "��X���b�g";
            }
        }
    }

    void OnLoadSlotClicked(int slot)
    {
        // SaveManager����Z�[�u�f�[�^�����[�h
        SaveData saveData = saveManager.LoadGame(slot);
        if (saveData == null)
        {
            Debug.LogWarning($"�X���b�g {slot} �ɃZ�[�u�f�[�^�����݂��܂���B");
            return;
        }

        // GameManager�ɏ�Ԃ𔽉f
        GameManager.instance.gameStateManager.CurrentStoryID = saveData.currentStoryIndex;
        GameManager.instance.gameStateManager.heroineName = saveData.heroineName;
        GameManager.instance.gameStateManager.loglist = saveData.loglist;

        Debug.Log($"�X���b�g {slot} �̃f�[�^�����[�h���܂����B");

        // �X�g�[���[�V�[���֑J��
        SceneManager.LoadScene("Mainscene_new2");
    }

    public void onclicked_returnbutton()
    {
        SceneManager.LoadScene("StartScene");
    }



}
