using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour
{
    public Button[] saveSlotButtons;

    private SaveManager saveManager;

    void Start()
    {
        // SaveManager���V�[�����ɑ��݂��邩�m�F
        saveManager = FindObjectOfType<SaveManager>();
        if (saveManager == null)
        {
            Debug.LogError("SaveManager ��������܂���B�V�[���� SaveManager ��z�u���Ă��������B");
            return;
        }

        // �e�X���b�g�{�^���ɃC�x���g��o�^
        for (int i = 0; i < saveSlotButtons.Length; i++)
        {
            int slot = i + 1; // �X���b�g�ԍ�
            saveSlotButtons[i].onClick.AddListener(() => OnSaveSlotClicked(slot));
        }
    }

    void OnSaveSlotClicked(int slot)
    {
        // GameManager���������ݒ肳��Ă��邩�m�F
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager �̃C���X�^���X��������܂���B");
            return;
        }

        // �K�v�ȃf�[�^���擾���ĕۑ�
        SaveData saveData = new SaveData
        {
            heroineName = GameManager.instance.heroinename,
            currentStoryIndex = GameManager.instance.storynum,
            affectionPoints = GameManager.instance.affectionPoints,
            screenshotPath = GameManager.instance.screenshotpath // �����̃X�N���[���V���b�g�p�X
        };

        saveManager.SaveGame(slot, saveData);

        Debug.Log($"�X���b�g {slot} �ɃQ�[�����Z�[�u���܂����B");
    }
}
