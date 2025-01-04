using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySystem : MonoBehaviour
{
    public UIManager uiManager; // UI�Ǘ��N���X
    public SoundManager soundManager; // �T�E���h�Ǘ��N���X
    public AutoPlayManager autoPlayManager; 

    private StoryManager storyManager; // �X�g�[���[�}�l�[�W��

    public GameStateManager gameStateManager = new GameStateManager();




    void Start()
    {
        GameManager.instance.previousSceneName = SceneManager.GetActiveScene().name; // ���݂̃V�[�������L�^

        // StoryManager�̏�����
        storyManager = new StoryManager();
        string csvPath = Application.dataPath + "/StreamingAssets/StoryCsv.csv";
        storyManager.LoadStories(csvPath);

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
        GameManager.instance.gameStateManager.CurrentStoryID++;
        if (GameManager.instance.gameStateManager.CurrentStoryID < storyManager.stories.Count)
        {
            DisplayStory(GameManager.instance.gameStateManager.CurrentStoryID);
        }
        else
        {
            Debug.Log("�X�g�[���[�I��");
            // �K�v�ɉ����ăV�[���J�ڂ�G���f�B���O����
        }
    }


    void OnDestroy()
    {
        // �C�x���g�̉���
        uiManager.OnMonthImageHidden -= NextStory;
    }
}
