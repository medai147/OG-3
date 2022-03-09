using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeScript : MonoBehaviour
{
    //アニメーター
    [SerializeField] private Animator _animator;

    // アニメーターコントローラーのレイヤー(通常は0)
    [SerializeField] private int _layer;

    //IsfadeStartフラグ
    private static readonly int ParamIsfadeStart = Animator.StringToHash("IsfadeStart");

    //fadeInisdoneフラグ
    private static readonly int fadeInisdone = Animator.StringToHash("fadeInisdone");

    //フェードフラグが立っているか
    public static bool isFadeOut = false;
    public static bool isFadeIn = false;

    //アニメーション中かどうか
    public bool IsTransition { get; private set; }

    //フェードイン
    public void fade()
    {
        //不正操作防止
        if (IsTransition) return;

        //IsfadeStartフラグをセット
        _animator.SetBool(ParamIsfadeStart, true);

        //アニメーション待機
        StartCoroutine(("start"));
    }

    // 開閉アニメーションの待機コルーチン
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
            // ステートが変化し、アニメーションが終了するまでループ
            var state = _animator.GetCurrentAnimatorStateInfo(_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsTransition = false;

        onCompleted?.Invoke();
    }
}

