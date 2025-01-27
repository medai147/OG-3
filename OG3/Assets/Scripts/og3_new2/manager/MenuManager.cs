using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logPanel; // ���O�p�l���̎Q��
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Scrollbar scrollbar; // Scrollbar�R���|�[�l���g

    bool deleteflag = false;
    GameObject[] deletes;

    /// <summary>
    /// �Z�[�u�V�[���ɑJ��
    /// </summary>
    public void GoToSaveScene()
    {
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
            scrollbar.value = 0.0f;

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
        if(!logPanel.activeSelf)
        {
            Time.timeScale = 1.0f;
            menuPanel.SetActive(false); // ���݂̃��j���[���\��
        }

    }

    public void onClicked_deletebutton()
    {
        deleteflag = true;
        deletes = GameObject.FindGameObjectsWithTag("delete");
        for (int i = 0; i < deletes.Length; i++)
        {
            deletes[i].SetActive(false);
        }
    }

    private void Update()
    {
        // �E�N���b�N�ō폜�����I�u�W�F�N�g��߂�����
        if (deleteflag && Input.GetMouseButtonDown(0)){
            deleteflag = false;
            for (int i = 0; i < deletes.Length; i++)
            {
                deletes[i].SetActive(true);
            }
        }


    }
}
