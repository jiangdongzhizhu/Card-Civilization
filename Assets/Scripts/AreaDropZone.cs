using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AreaDropZone : MonoBehaviour, IDropHandler
{
    private Area area;

    private void Awake()
    {
        area = GetComponent<Area>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        area.PlayCard();
        HandCard card = eventData.pointerDrag.GetComponent<HandCard>();
        card.Play();
    }
}
