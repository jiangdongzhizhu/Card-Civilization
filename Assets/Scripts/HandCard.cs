using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Text nameText;
    private RectTransform canvasRectTransform;

    private CardInfo cardInfo;

    private void Awake()
    {
        canvasRectTransform = GetComponentInParent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRectTransform, eventData.pressPosition, Camera.main, out var from);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRectTransform, eventData.position, Camera.main, out var to);
        LineRendererInstance.Inst.SetLinePositions(from, to);
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        LineRendererInstance.Inst.ClearLine();
    }

    public void SetInfo(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        nameText.text = cardInfo.cardName;
    }

    public void Play()
    {
        CardManager.Inst.PlayCard(cardInfo);
        Discard();
    }

    public void Discard()
    {
        Destroy(gameObject);
    }
}
