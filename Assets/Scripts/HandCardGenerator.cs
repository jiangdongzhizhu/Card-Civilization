using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCardGenerator : MonoBehaviour
{
    [SerializeField] private Transform handRoot;
    [SerializeField] private GameObject handCardPrefab;
    [SerializeField] private Text drawPileCountText;
    [SerializeField] private Text discardPileCountText;

    private void Start()
    {
        CardManager.Inst.AfterTurnEnd += GenerateCards;
        CardManager.Inst.AfterPlayCard += UpdateDiscardPileCountText;
    }

    private void GenerateCards()
    {
        foreach (var handCard in handRoot.GetComponentsInChildren<HandCard>())
        {
            handCard.Discard();
        }

        foreach (var cardInfo in CardManager.Inst.Hand)
        {
            var instance = Instantiate(handCardPrefab, handRoot);
            HandCard handCard = instance.GetComponentInChildren<HandCard>();
            handCard.SetInfo(cardInfo);
        }

        drawPileCountText.text = CardManager.Inst.DrawPile.Count.ToString();
        UpdateDiscardPileCountText();
    }

    private void UpdateDiscardPileCountText()
    {
        discardPileCountText.text = CardManager.Inst.DiscardPile.Count.ToString();
    }
}
