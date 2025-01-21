using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text storyText;
    public Text nameText;
    public Image backgroundImage;
    public Image centerCharacterImage;
    public Image leftCharacterImage;
    public Image rightCharacterImage;
    public Image textBoxImage;
    public Button nextButton;

    public GameObject logPanel; // ���O��ʃp�l��
    public Text monthText;
    public Text logContent; // ���O��ʂ̃e�L�X�g�\���G���A

    private List<string> logEntries = new List<string>(); // ���O��ۑ����郊�X�g
    private Coroutine textDisplayCoroutine;
    private bool isTextDisplaying = false;
    private string currentFullText = ""; // ���ݕ\������S��

    public Image stillImage; // �X�`���摜�\���p
    public Image monthImage; // ���؂�ւ��摜�\���p

    //���؂�ւ��̍ۂɎ��̍s�ɐi�ނ悤�ɂ���
    public delegate void MonthImageHiddenHandler();
    public event MonthImageHiddenHandler OnMonthImageHidden;

    GameStateManager gameStateManager = new GameStateManager();


    public bool IsTextFullyDisplayed => !isTextDisplaying; // �e�L�X�g�\�������ǂ������m�F




    public void UpdateUI(StoryData story)
    {
        // ���O�ƃe�L�X�g�{�b�N�X���X�V
        string displayedName = story.name == "����" ? GameManager.instance.gameStateManager.heroineName :story.name;
        displayedName = story.name == "clear" ? "" : displayedName;
        nameText.text = displayedName;
        UpdateTextBoxImage(story.name);

        monthText.text = gameStateManager.GetMonth(story.number);

        // �w�i�ƃL�����N�^�[�摜���X�V
        backgroundImage.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/" + story.backimage);
        centerCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.centercharacterimage);
        leftCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.leftcharacterimage);
        rightCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.rightcharacterimage);

        // ���O�ɒǉ�
        AddToLog(displayedName, story.mainstory);

        // �X�`���摜�̕\��
        if (!string.IsNullOrEmpty(story.stillimage) && story.stillimage != "0")
        {
            ShowStillImage(story.stillimage);
        }
        else
        {
            HideStillImage();
        }

        // ���؂�ւ��摜�̕\��
        if (!string.IsNullOrEmpty(story.monthimage) && story.monthimage != "0")
        {
            ShowMonthImage(story.monthimage);
        }


        // �e�L�X�g��\��
        StartTextTyping(story.mainstory);
    }

    void UpdateTextBoxImage(string name)
    {
        string textBoxSpritePath = "Sprites/UI/";
        switch (name)
        {
            case "����": textBoxSpritePath += "text_own"; break;
            case "�ڋ� ��": textBoxSpritePath += "text_hikaru"; break;
            case "���R ����": textBoxSpritePath += "text_ouji"; break;
            case "�{�c ���J��": textBoxSpritePath += "text_rukia"; break;
            case "": textBoxSpritePath += "text_monologue"; break;
            case "clear":textBoxSpritePath += "clear"; break;
            default: textBoxSpritePath += "text_mob"; break;
        }
        textBoxImage.sprite = Resources.Load<Sprite>(textBoxSpritePath);
    }

    void AddToLog(string name, string text)
    {
        logEntries.Add($"<b>{name}:</b> {text}");
        UpdateLogPanel();
    }

    void UpdateLogPanel()
    {
        logContent.text = string.Join("\n\n", logEntries);
    }

    public void ToggleLogPanel()
    {
        logPanel.SetActive(!logPanel.activeSelf);
    }

    public void StartTextTyping(string text)
    {
        if (textDisplayCoroutine != null)
        {
            StopCoroutine(textDisplayCoroutine);
        }
        currentFullText = text; // �S����ۑ�
        textDisplayCoroutine = StartCoroutine(DisplayTextWithTypingEffect(text));
    }

    IEnumerator DisplayTextWithTypingEffect(string text)
    {
        storyText.text = "";
        isTextDisplaying = true;

        float minSpeed = 0.2f; // �ł��x�����x
        float maxSpeed = 0.03f; // �ł��������x

        float speedFactor = SettingManager.Instance.TextSpeed; // �͈� [1, 3]
        float typingSpeed = Mathf.Lerp(minSpeed, maxSpeed, (speedFactor - 1) / 2);

        foreach (char c in text)
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTextDisplaying = false; // �\������
    }

    public void FinishTextTyping()
    {
        if (textDisplayCoroutine != null)
        {
            StopCoroutine(textDisplayCoroutine);
            textDisplayCoroutine = null;
        }
        storyText.text = currentFullText; // �S����\��
        isTextDisplaying = false;
    }

    public void OnNextButtonClick(UnityEngine.Events.UnityAction nextAction)
    {
        if (isTextDisplaying)
        {
            FinishTextTyping();
        }
        else
        {
            nextAction.Invoke();
        }
    }


    public void SetNextButtonAction(UnityEngine.Events.UnityAction action)
    {
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => OnNextButtonClick(action));
    }



    void ShowStillImage(string imagePath)
    {
        stillImage.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/" + imagePath);
        stillImage.gameObject.SetActive(true);
    }

    void HideStillImage()
    {
        stillImage.gameObject.SetActive(false);
    }


    void ShowMonthImage(string imagePath)
    {
        monthImage.sprite = Resources.Load<Sprite>("Sprites/month/" + imagePath);
        monthImage.gameObject.SetActive(true);

        // �����I�ɔ�\���ɂ���ꍇ�i�K�v�ł���΁j
        Invoke(nameof(HideMonthImage), 2f); // 2�b��ɔ�\��
    }

    void HideMonthImage()
    {
        monthImage.gameObject.SetActive(false);

        // �C�x���g�𔭉�
        OnMonthImageHidden?.Invoke();
    }




    void OnEnable()
    {
        Time.timeScale = 1; // ���Ԃ𐳏퉻
    }
}
