using UnityEngine;
using UnityEngine.UI;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private GameObject moveImage; // �ꏊ�\���p�̃I�u�W�F�N�g
    [SerializeField] private Text moveText; // moveImage����Text�R���|�[�l���g

    /// <summary>
    /// �ꏊ����ݒ肵�ĕ\��
    /// </summary>
    /// <param name="locationName">�\������ꏊ��</param>
    public void ShowMove(string locationName)
    {
        if (!string.IsNullOrEmpty(locationName))
        {
            moveText.text = locationName; // �e�L�X�g��ݒ�
            moveImage.SetActive(true);   // �\��
        }
        else
        {
            moveImage.SetActive(false); // ��\��
        }
    }

    /// <summary>
    /// �����I�ɔ�\���ɂ���
    /// </summary>
    public void HideMove()
    {
        if (moveImage != null)
        {
            moveImage.SetActive(false);
        }
        
    }
}
