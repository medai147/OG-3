using UnityEngine;

public class FadeAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator; // Animator���A�^�b�`
    [SerializeField] private GameObject FadePanel;
    private System.Action onFadeComplete; // �t�F�[�h�������̃R�[���o�b�N

    /// <summary>
    /// �t�F�[�h�A�j���[�V�������Đ�
    /// </summary>
    /// <param name="fadeType">"in" �܂��� "out" ���w��</param>
    /// <param name="onComplete">�t�F�[�h������ɌĂяo���A�N�V����</param>
    public void PlayFadeAnimation(string fadeType, System.Action onComplete = null)
    {
        if (fadeAnimator == null)
        {
            Debug.LogError("Fade Animator ���ݒ肳��Ă��܂���I");
            return;
        }

        onFadeComplete = onComplete; // �R�[���o�b�N��o�^

        if (fadeType == "in")
        {
            FadePanel.SetActive(true);
            fadeAnimator.SetTrigger("FadeIn");
        }
        else if (fadeType == "out")
        {
            FadePanel.SetActive(true);
            fadeAnimator.SetTrigger("FadeOut");
        }
        else
        {
            Debug.LogWarning("�����ȃt�F�[�h�^�C�v���w�肳��܂���: " + fadeType);
        }
    }

    /// <summary>
    /// �t�F�[�h�A�j���[�V�����������ɌĂяo����郁�\�b�h
    /// </summary>
    public void OnFadeComplete()
    {
        FadePanel.SetActive(false);
        onFadeComplete?.Invoke(); // �R�[���o�b�N�����s
    }
}
