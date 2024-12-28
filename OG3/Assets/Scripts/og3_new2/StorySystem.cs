using UnityEngine;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI�Ǘ��N���X
    public SoundManager soundManager; // �T�E���h�Ǘ��N���X

    private StoryManager storyManager; // �X�g�[���[�}�l�[�W��
    private int currentStoryIndex = 1; // ���݂̃X�g�[���[�ԍ�

    public GameStateManager gameStateManager = new GameStateManager();


    void Start()
    {
        // StoryManager�̏�����
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

        // UIManager�̌��摜��\���C�x���g��o�^
        uiManager.OnMonthImageHidden += NextStory;

        // �ŏ��̃X�g�[���[��\��
        DisplayStory(gameStateManager.CurrentStoryID);

        // UI�{�^���̃N���b�N������o�^
        uiManager.SetNextButtonAction(NextStory);
    }

    void DisplayStory(int storyIndex)
    {
        StoryData story = storyManager.GetStory(storyIndex);

        if (story != null)
        {
            // UI�ƃI�[�f�B�I���X�V
            uiManager.UpdateUI(story);
            soundManager.PlayAudio(story.bgm, story.se);
        }
        else
        {
            Debug.LogError("�X�g�[���[�f�[�^��������܂���: " + storyIndex);
        }
    }

    public void NextStory()
    {
        gameStateManager.CurrentStoryID++;
        if (gameStateManager.CurrentStoryID < storyManager.stories.Count)
        {
            DisplayStory(gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("�X�g�[���[�I��");
            // �K�v�ɉ����ăV�[���J�ڂ�G���f�B���O����
        }
    }


    void OnDestroy()
    {
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
