using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logPanel; // ���O�p�l���̎Q��
    [SerializeField] private GameObject menuPanel;

    /// <summary>
    /// �Z�[�u�V�[���ɑJ��
    /// </summary>
    public void GoToSaveScene()
    {
        // �K�v�ɉ����Č��݂̏�Ԃ�ۑ����鏈����ǉ��\
        SceneManager.LoadScene("savescene_new2"); // �Z�[�u�V�[���ɑJ��
    }

    /// <summary>
    /// ���[�h�V�[���ɑJ��
    /// </summary>
    public void GoToLoadScene()
    {
        SceneManager.LoadScene("loadscene"); // ���[�h�V�[���ɑJ��
    }

    /// <summary>
    /// �^�C�g���V�[���ɑJ��
    /// </summary>
    public void GoToTitleScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("startscene_new2"); // ���[�h�V�[���ɑJ��
    }

    /// <summary>
    /// ���O�p�l���̕\��/��\����؂�ւ���
    /// </summary>
    public void ToggleLogPanel()
    {
        if (logPanel != null)
        {
            logPanel.SetActive(!logPanel.activeSelf); // �\��/��\����؂�ւ�
        }
        else
        {
            Debug.LogError("���O�p�l�����ݒ肳��Ă��܂���I");
        }
    }

    /// <summary>
    /// ���j���[��ʂ����
    /// </summary>
    public void CloseMenu()
    {
        Time.timeScale = 1.0f;
        menuPanel.SetActive(false); // ���݂̃��j���[���\��
    }
}
