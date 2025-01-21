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

    public GameObject logPanel; // ログ画面パネル
    public Text monthText;
    public Text logContent; // ログ画面のテキスト表示エリア

    private List<string> logEntries = new List<string>(); // ログを保存するリスト
    private Coroutine textDisplayCoroutine;
    private bool isTextDisplaying = false;
    private string currentFullText = ""; // 現在表示する全文

    public Image stillImage; // スチル画像表示用
    public Image monthImage; // 月切り替え画像表示用

    //月切り替えの際に次の行に進むようにする
    public delegate void MonthImageHiddenHandler();
    public event MonthImageHiddenHandler OnMonthImageHidden;

    GameStateManager gameStateManager = new GameStateManager();


    public bool IsTextFullyDisplayed => !isTextDisplaying; // テキスト表示中かどうかを確認




    public void UpdateUI(StoryData story)
    {
        // 名前とテキストボックスを更新
        string displayedName = story.name == "えみ" ? GameManager.instance.gameStateManager.heroineName :story.name;
        displayedName = story.name == "clear" ? "" : displayedName;
        nameText.text = displayedName;
        UpdateTextBoxImage(story.name);

        monthText.text = gameStateManager.GetMonth(story.number);

        // 背景とキャラクター画像を更新
        backgroundImage.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/" + story.backimage);
        centerCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.centercharacterimage);
        leftCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.leftcharacterimage);
        rightCharacterImage.sprite = Resources.Load<Sprite>("Sprites/Characters/" + story.rightcharacterimage);

        // ログに追加
        AddToLog(displayedName, story.mainstory);

        // スチル画像の表示
        if (!string.IsNullOrEmpty(story.stillimage) && story.stillimage != "0")
        {
            ShowStillImage(story.stillimage);
        }
        else
        {
            HideStillImage();
        }

        // 月切り替え画像の表示
        if (!string.IsNullOrEmpty(story.monthimage) && story.monthimage != "0")
        {
            ShowMonthImage(story.monthimage);
        }


        // テキストを表示
        StartTextTyping(story.mainstory);
    }

    void UpdateTextBoxImage(string name)
    {
        string textBoxSpritePath = "Sprites/UI/";
        switch (name)
        {
            case "えみ": textBoxSpritePath += "text_own"; break;
            case "目鏡 光": textBoxSpritePath += "text_hikaru"; break;
            case "犬山 桜児": textBoxSpritePath += "text_ouji"; break;
            case "本田 琉絆空": textBoxSpritePath += "text_rukia"; break;
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
        currentFullText = text; // 全文を保存
        textDisplayCoroutine = StartCoroutine(DisplayTextWithTypingEffect(text));
    }

    IEnumerator DisplayTextWithTypingEffect(string text)
    {
        storyText.text = "";
        isTextDisplaying = true;

        float minSpeed = 0.2f; // 最も遅い速度
        float maxSpeed = 0.03f; // 最も速い速度

        float speedFactor = SettingManager.Instance.TextSpeed; // 範囲 [1, 3]
        float typingSpeed = Mathf.Lerp(minSpeed, maxSpeed, (speedFactor - 1) / 2);

        foreach (char c in text)
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTextDisplaying = false; // 表示完了
    }

    public void FinishTextTyping()
    {
        if (textDisplayCoroutine != null)
        {
            StopCoroutine(textDisplayCoroutine);
            textDisplayCoroutine = null;
        }
        storyText.text = currentFullText; // 全文を表示
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

        // 自動的に非表示にする場合（必要であれば）
        Invoke(nameof(HideMonthImage), 2f); // 2秒後に非表示
    }

    void HideMonthImage()
    {
        monthImage.gameObject.SetActive(false);

        // イベントを発火
        OnMonthImageHidden?.Invoke();
    }




    void OnEnable()
    {
        Time.timeScale = 1; // 時間を正常化
    }
}
