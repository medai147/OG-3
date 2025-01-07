using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image buttonImage; // ボタンの画像
    [SerializeField] private Sprite defaultSprite; // デフォルトのスプライト
    [SerializeField] private Sprite hoverSprite; // ホバー時のスプライト

    void Start()
    {
        if (buttonImage == null)
        {
            buttonImage = GetComponent<Image>();
        }

        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    // マウスがボタンに乗ったときに呼び出される
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    // マウスがボタンから離れたときに呼び出される
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}