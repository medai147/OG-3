using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator CharacterAnimator; // Animator���A�^�b�`

    public void PlayCharacterAnimation(string characterType, System.Action onComplete = null)
    {
        if (CharacterAnimator == null)
        {
            Debug.LogError("Charactor Animator ���ݒ肳��Ă��܂���I");
            return;
        }


        if (characterType != "")
        {
            CharacterAnimator.SetTrigger(characterType);
        }
        else
        {
            CharacterAnimator.CrossFade("New State", 0.1f);
            Debug.LogWarning("�����ȃt�F�[�h�^�C�v���w�肳��܂���: " + characterType);
        }
    }
}
