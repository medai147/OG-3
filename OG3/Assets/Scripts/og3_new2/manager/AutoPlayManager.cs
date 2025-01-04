using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayManager : MonoBehaviour
{
    private bool isAutoPlaying = false; // �I�[�g�Đ������ǂ���
    private Coroutine autoPlayCoroutine;
    private StorySystem storySystem;
    [SerializeField] private Button autoPlayButton;
    [SerializeField] private Button nextButton; // ���̃{�^��

    // �f�t�H���g�ƃI�[�g���̐F���`
    private readonly Color defaultColor = new Color32(50, 50, 50, 255); // #323232
    private readonly Color autoColor = new Color32(255, 57, 222, 255);  // #FF39DE

    [SerializeField] private Text autoModeText; // �I�[�g���[�h�̏�Ԃ�\������e�L�X�g
    public void Initialize(StorySystem storySystem)
    {
        this.storySystem = storySystem;

        if (this.storySystem == null)
        {
            Debug.LogError("StorySystem ������������Ă��܂���B");
        }
    }


    public void ToggleAutoPlay()
    {
        Debug.Log("������");
        if (isAutoPlaying)
        {
            StopAutoPlay();
        }
        else
        {
            StartAutoPlay();
        }
        UpdateAutoModeTextColor(); // �e�L�X�g�̐F���X�V
        UpdateNextButtonState();  // nextButton �̏�Ԃ��X�V
    }

    private void StartAutoPlay()
    {
        isAutoPlaying = true;
        autoPlayCoroutine = StartCoroutine(AutoPlayCoroutine());
    }

    private void StopAutoPlay()
    {
        if (autoPlayCoroutine != null)
        {
            StopCoroutine(autoPlayCoroutine);
            autoPlayCoroutine = null;
        }
        isAutoPlaying = false;
    }

    private IEnumerator AutoPlayCoroutine()
    {
        while (isAutoPlaying)
        {
            // �e�L�X�g���\����������܂őҋ@
            while (!storySystem.uiManager.IsTextFullyDisplayed)
            {
                yield return null; // ���̃t���[���܂őҋ@
            }

            // �\��������A��莞�ԑҋ@
            yield return new WaitForSeconds(GetAutoPlayDelay());

            // ���̃X�g�[���[�֐i��
            storySystem.NextStory();
        }
    }


    private float GetAutoPlayDelay()
    {
        // �ݒ�l�ɉ������x�����Ԃ��擾
        switch (SettingManager.Instance.AutoSpeed)
        {
            case 1: return 2.0f; // �x��
            case 2: return 1.0f; // ����
            case 3: return 0.5f; // ����
            default: return 1.0f;
        }
    }

    public void SetAutoPlayButtonAction(UnityEngine.Events.UnityAction action)
    {
        autoPlayButton.onClick.RemoveAllListeners();
        autoPlayButton.onClick.AddListener(action);
    }

    private void UpdateAutoModeTextColor()
    {
        if (autoModeText != null)
        {
            autoModeText.color = isAutoPlaying ? autoColor : defaultColor; // ��Ԃɉ����ĐF��ݒ�
        }
    }

    private void UpdateNextButtonState()
    {
        if (nextButton != null)
        {
            nextButton.interactable = !isAutoPlaying; // �I�[�g���͖�����
        }
    }

}
