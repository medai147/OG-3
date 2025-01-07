using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image buttonImage; // �{�^���̉摜
    [SerializeField] private Sprite defaultSprite; // �f�t�H���g�̃X�v���C�g
    [SerializeField] private Sprite hoverSprite; // �z�o�[���̃X�v���C�g

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

    // �}�E�X���{�^���ɏ�����Ƃ��ɌĂяo�����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && hoverSprite != null)
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    // �}�E�X���{�^�����痣�ꂽ�Ƃ��ɌĂяo�����
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}