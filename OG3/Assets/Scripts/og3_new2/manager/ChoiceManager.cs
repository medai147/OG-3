using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public List<ChoiceData> choices = new List<ChoiceData>();

    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private Transform choiceContainer;
    [SerializeField] private GameObject backImage;

    private ChoiceData currentChoice;

    public void LoadChoices(string csvFileName)
    {
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);

        if (csvData == null)
        {
            Debug.LogError("�w�肳�ꂽCSV�t�@�C����������܂���: " + csvFileName);
            return;
        }

        string[] lines = csvData.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // �w�b�_�[�s���X�L�b�v
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] data = line.Split(',');

            if (data.Length < 7) // �J����������Ă��邩�m�F
            {
                Debug.LogError($"�f�[�^�񂪕s�����Ă��܂� (�s {i + 1}): {line}");
                continue;
            }

            try
            {
                ChoiceData choice = new ChoiceData
                {
                    SelectID = TryParseInt(data[0], i + 1, "SelectID"),
                    Text = data[1],
                    NextSceneID = TryParseInt(data[2], i + 1, "NextSceneID"),
                    EndRange = TryParseInt(data[3], i + 1, "EndRange"),
                    TargetCharacter = data[4],
                    AffectionChange = TryParseInt(data[5], i + 1, "AffectionChange"),
                    Condition = string.IsNullOrEmpty(data[6]) ? null : data[6]
                };

                choices.Add(choice);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"�I�����f�[�^�̉�͂Ɏ��s���܂��� (�s {i + 1}): {line} - {ex.Message}");
            }
        }

        Debug.Log($"�I�����f�[�^��ǂݍ��݂܂���: {choices.Count}��");
    }

    private int TryParseInt(string value, int lineNumber, string fieldName)
    {
        if (int.TryParse(value, out int result))
        {
            return result;
        }
        else
        {
            Debug.LogError($"���l�ϊ��G���[: {fieldName} (�s {lineNumber}): \"{value}\"");
            return 0; // �f�t�H���g�l��Ԃ�
        }
    }



// �w�肵���X�g�[���[�̑I������\������
public void DisplayChoicesForStory(int storySelectID)
    {
        if (storySelectID <= 0)
        {
            Debug.Log("�I����ID��0�܂��͖����̂��߁A�I�����͕\������܂���B");
            return;
        }

        // �Y������I�������擾
        List<ChoiceData> availableChoices = GetChoicesForSelectID(storySelectID);

        if (availableChoices == null || availableChoices.Count == 0)
        {
            Debug.LogWarning($"�I����ID {storySelectID} �ɑΉ�����I������������܂���B");
            return;
        }

        backImage.SetActive(true);
        // �I������UI�ɕ\��
        float buttonSpacing = 80f; // �{�^���Ԃ̃X�y�[�X
        float startY = 30f; // �{�^���̏���Y�ʒu
        int index = 0; // �{�^���̃C���f�b�N�X

        foreach (ChoiceData choice in availableChoices)
        {
            GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);

            // �{�^���e�L�X�g��ݒ�
            Text buttonText = choiceButton.GetComponentInChildren<Text>();
            buttonText.text = choice.Text;

            // �{�^���̈ʒu�𒲐�
            RectTransform rectTransform = choiceButton.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(0, startY - (buttonSpacing * index));
            }

            // �{�^���̃N���b�N�C�x���g��ݒ�
            choiceButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnChoiceSelected(choice);
            });

            index++;
        }
    }


    // �w�肵���I����ID�ɑΉ�����I�������X�g��Ԃ�
    public List<ChoiceData> GetChoicesForSelectID(int selectID)
    {
        List<ChoiceData> validChoices = new List<ChoiceData>();

        foreach (ChoiceData choice in choices)
        {
            if (choice.SelectID == selectID)
            {
                if (string.IsNullOrEmpty(choice.Condition) || EvaluateCondition(choice.Condition))
                {
                    validChoices.Add(choice);
                }
            }
        }

        return validChoices;
    }

    // ������]�����郁�\�b�h
    private bool EvaluateCondition(string condition)
    {
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();

        if (characterManager != null && condition.Contains(">"))
        {
            string[] parts = condition.Split('>');
            string character = parts[0];
            int threshold = int.Parse(parts[1]);

            return characterManager.GetAffection(character) > threshold;
        }

        return true; // �f�t�H���g�ŏ����𖞂����Ƃ���
    }

    // �I�������I�΂ꂽ���̏���
    public void OnChoiceSelected(ChoiceData choice)
    {
        currentChoice = choice; // ���݂̑I�������L�^

        if (!string.IsNullOrEmpty(choice.TargetCharacter))
        {
            CharacterManager characterManager = FindObjectOfType<CharacterManager>();
            characterManager.UpdateAffection(choice.TargetCharacter, choice.AffectionChange);
        }

        // �w�i�摜���\��
        backImage.SetActive(false);

        // �I����UI���N���A
        ClearChoicesUI();

        // �I������NextSceneID�ɃW�����v
        GameManager.instance.gameStateManager.CurrentStoryID = choice.NextSceneID;
        StorySystem storySystem = FindObjectOfType<StorySystem>();

        storySystem.DisplayStory(choice.NextSceneID);
    }


    public void ClearChoicesUI()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject); // �q�I�u�W�F�N�g�i�I�����{�^���j���폜
        }
    }

    public int GetMaxEndRange(int selectID)
    {
        return choices.Where(c => c.SelectID == selectID).Max(c => c.EndRange);
    }

    public ChoiceData GetCurrentChoice()
    {
        return currentChoice;
    }



}

// �I�����f�[�^��ێ�����N���X
public class ChoiceData
{
    public int SelectID;
    public string Text;
    public int NextSceneID;
    public string TargetCharacter;
    public int AffectionChange;
    public string Condition;
    public int EndRange; // �I���ԍ�
}
