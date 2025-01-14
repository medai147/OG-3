using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    private List<ChoiceData> choices = new List<ChoiceData>();

    [SerializeField] private GameObject choiceButtonPrefab; // �I�����{�^���̃v���n�u
    [SerializeField] private Transform choiceContainer; // �I�����{�^����z�u����R���e�i
    [SerializeField] private GameObject backImage;

    private ChoiceData currentChoice;

    // CSV�f�[�^�̓ǂݍ���
    public void LoadChoices(string csvPath)
    {
        if (!File.Exists(csvPath))
        {
            Debug.LogError("�I����CSV�t�@�C����������܂���: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 1; i < lines.Length; i++) // �w�b�_�[�s���X�L�b�v
        {
            string[] data = lines[i].Split(',');

            try
            {
                ChoiceData choice = new ChoiceData
                {
                    SelectID = int.Parse(data[0]),
                    Text = data[1],
                    NextSceneID = int.Parse(data[2]),
                    EndRange = int.Parse(data[3]), // CSV����I���͈͂��擾
                    TargetCharacter = data[4],
                    AffectionChange = int.Parse(data[5]),
                    Condition = string.IsNullOrEmpty(data[6]) ? null : data[6]
                };

                choices.Add(choice);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"�I�����f�[�^�̉�͂Ɏ��s���܂��� (�s {i + 1}): {ex.Message}");
            }
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

        //�t�F�[�h�����̂��߂ɃX�g�[���[�ԍ������ꂽ����+2���Ă����ŏC��
        storySystem.DisplayStory(choice.NextSceneID + 2);
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
        int maxEndRange = -1;

        foreach (ChoiceData choice in choices)
        {
            if (choice.SelectID == selectID)
            {
                if (choice.EndRange > maxEndRange)
                {
                    maxEndRange = choice.EndRange;
                }
            }
        }

        return maxEndRange;
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
