using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonHandler : MonoBehaviour
{
    public void onclicked_returnbutton()
    {
        string previousScene = GameManager.instance.previousSceneName;

        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene); // �L�^���ꂽ�V�[�����ɖ߂�
            GameManager.instance.previousSceneName = "";
        }
        else
        {
            Debug.LogWarning("�O�̃V�[�������L�^����Ă��܂���B�f�t�H���g�̃V�[���ɖ߂�܂��B");
            SceneManager.LoadScene("StartScene"); // �f�t�H���g�̖߂��
        }
    }
}
