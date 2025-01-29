using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI�Ǘ��N���X
    public SoundManager soundManager; // �T�E���h�Ǘ��N���X
    public AutoPlayManager autoPlayManager; 
    public FadeAnimationManager fadeAnimationManager;
    public MoveManager moveManager; // MoveManager���Q��
    public ScreenAnimationManager screenAnimationManager;
    public CharacterAnimationManager characterAnimationManager;

    private StoryManager storyManager; // �X�g�[���[�}�l�[�W��

    public GameStateManager gameStateManager = new GameStateManager();


    private int lastSkippedStoryIndex = -1; // �Ō�ɃX�L�b�v�����X�g�[���[�ԍ����L�^

    void Start()
    {
        GameManager.instance.previousSceneName = SceneManager.GetActiveScene().name; // ���݂̃V�[�������L�^

        // StoryManager�̏�����
        storyManager = new StoryManager();
        string storyCsvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(storyCsvPath);

        // ChoiceManager�̏�����
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (choiceManager != null)
        {
            // �I�����f�[�^��CSV�p�X���w��
            string choiceCsvPath = Application.dataPath + "/StreamingAssets/SelectCsv.csv";
            choiceManager.LoadChoices(choiceCsvPath);
        }
        else
        {
            Debug.LogError("ChoiceManager ��������܂���B");
        }

        // UIManager�̌��摜��\���C�x���g��o�^
        uiManager.OnMonthImageHidden += NextStory;

        uiManager.SetNextButtonAction(NextStory);

        // AutoPlayManager �̏�����
        if (autoPlayManager != null)
        {
            autoPlayManager.Initialize(this);
        }
        else
        {
            Debug.LogError("AutoPlayManager ���ݒ肳��Ă��܂���B");
        }

        // �ŏ��̃X�g�[���[��\��
        DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
    }


    public void DisplayStory(int storyIndex)
    {
        // �X�g�[���[�f�[�^���擾
        StoryData story = storyManager.GetStory(storyIndex);

        if (story != null)
        {

            screenAnimationManager.PlayScreenAnimation(story.screenanimation);


            //characteranimation���w�肳��Ă���ꍇ
            characterAnimationManager.PlayCharacterAnimation(story.characteranimation);


            // �t�F�[�h�A�j���[�V�������Ȃ��ꍇ�͒���UI�ƃI�[�f�B�I���X�V
            UpdateStoryUIAndChoices(story);
            // moveanimation����m�F����MoveManager���X�V
            if (!string.IsNullOrEmpty(story.moveanimation))
            {
                // 140�b��Ɏ��̃X�g�[���[�ɐi��
                StartCoroutine(WaitAndProceed(2.13f));
                autoPlayManager.StopAutoPlay();
                moveManager.ShowMove(story.moveanimation);

                return; 
            }

            // fadeanimation���w�肳��Ă���ꍇ
            if (!string.IsNullOrEmpty(story.fadeanimation))
            {
                // �t�F�[�h�J�n���ɃI�[�g���[�h���~
                autoPlayManager.StopAutoPlay();

                fadeAnimationManager.PlayFadeAnimation(story.fadeanimation, () =>
                {
                    // �t�F�[�h�A�j���[�V����������������UI�ƃI�[�f�B�I���X�V
                    UpdateStoryUIAndChoices(story);
                    NextStory();
                });
            }

        }
        else
        {
            //�I������
            SceneManager.LoadScene("FinishScene2");

            Debug.LogError("�X�g�[���[�f�[�^��������܂���: " + storyIndex);
        }
    }

    /// <summary>
    /// �w��b���ҋ@��Ɏ��̃X�g�[���[��i�߂�
    /// </summary>
    private IEnumerator WaitAndProceed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // �w�莞�ԑҋ@
        moveManager.HideMove();
        NextStory(); // ���̃X�g�[���[�ɐi��
    }

    /// <summary>
    /// UI�A�I�[�f�B�I�̍X�V�ƑI�����\�����s��
    /// </summary>
    /// <param name="story">�X�V����X�g�[���[�f�[�^</param>
    private void UpdateStoryUIAndChoices(StoryData story)
    {
        // UI�ƃI�[�f�B�I���X�V
        uiManager.UpdateUI(story);
        soundManager.PlayAudio(story.bgm, story.se);

        // �I����������ꍇ�͑I������\��
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (story.selectid > 0 && choiceManager != null)
        {
            // �t�F�[�h�J�n���ɃI�[�g���[�h���~
            autoPlayManager.StopAutoPlay();
            // �ߋ��̑I�������N���A
            choiceManager.ClearChoicesUI();

            // ���݂̑I������\��
            choiceManager.DisplayChoicesForStory(story.selectid);
        }
    }




    public void NextStory()
    {
        // ���݂̃X�g�[���[�ԍ����擾
        int currentStoryIndex = GameManager.instance.gameStateManager.CurrentStoryID;

        // ���݂̑I�����͈̔͂��擾
        ChoiceManager choiceManager = FindObjectOfType<ChoiceManager>();
        if (choiceManager != null)
        {
            // ���݂� `currentStoryID` ���ǂ� `end` �Ɉ�v���邩�`�F�b�N
            foreach (ChoiceData choice in choiceManager.choices)
            {
                if (currentStoryIndex == choice.EndRange)
                {
                    int selectId = choice.SelectID;

                    // ���� selectID ������ `EndRange` �̍ő�l���擾
                    int maxEndRange = choiceManager.GetMaxEndRange(selectId);


                    // �X�L�b�v����
                    GameManager.instance.gameStateManager.CurrentStoryID = maxEndRange + 1;
  
                    Debug.Log($"�X�g�[���[�͈͂��X�L�b�v���� {maxEndRange + 1} �ֈړ����܂��B");

                    DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
                    return;
                }
            }
        }
        // ���̃X�g�[���[�֐i��
        GameManager.instance.gameStateManager.CurrentStoryID++;
        if (GameManager.instance.gameStateManager.CurrentStoryID <= storyManager.stories.Count)
        {
            DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("�X�g�[���[�I��");
            // �K�v�ɉ����ăG���f�B���O������V�[���J��
        }
    }



    void OnDestroy()
    {
        // �C�x���g�̉���
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
