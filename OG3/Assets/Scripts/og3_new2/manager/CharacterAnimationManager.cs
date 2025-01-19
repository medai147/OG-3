using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator CharacterAnimator; // Animatorをアタッチ

    public void PlayCharacterAnimation(string characterType, System.Action onComplete = null)
    {
        if (CharacterAnimator == null)
        {
            Debug.LogError("Charactor Animator が設定されていません！");
            return;
        }


        if (characterType != "")
        {
            CharacterAnimator.SetTrigger(characterType);
        }
        else
        {
            CharacterAnimator.CrossFade("New State", 0.1f);
            Debug.LogWarning("無効なフェードタイプが指定されました: " + characterType);
        }
    }
}
