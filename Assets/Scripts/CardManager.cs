using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardInfo
{
    public string cardName;

    public CardInfo(string cardName)
    {
        this.cardName = cardName;
    }
}

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }

    private List<CardInfo> drawPile = new List<CardInfo>();
    private List<CardInfo> hand = new List<CardInfo>();
    private List<CardInfo> discardPile = new List<CardInfo>();

    public IReadOnlyList<CardInfo> DrawPile => drawPile;
    public IReadOnlyList<CardInfo> Hand => hand;
    public IReadOnlyList<CardInfo> DiscardPile => discardPile;

    public event Action AfterTurnEnd;
    public event Action AfterPlayCard;

    private void Awake()
    {
        Inst = this;

        drawPile.AddRange(new CardInfo[] {
            new CardInfo("farm"),
            new CardInfo("farm"),
            new CardInfo("farm"),
            new CardInfo("mine"),
            new CardInfo("mine"),
            new CardInfo("mine"),
            new CardInfo("seaport"),
            new CardInfo("seaport"),
            new CardInfo("market"),
            new CardInfo("library")
        });
    }

    private void Start()
    {
        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
        {
            yield return new WaitForFixedUpdate();
            EndTurn();
        }
    }

    public void PlayCard(CardInfo cardInfo)
    {
        GlobalManager.TargetAreaInfo.BuildBuilding(cardInfo.cardName);
        hand.Remove(cardInfo);
        discardPile.Add(cardInfo);
        AfterPlayCard?.Invoke();
    }

    public void EndTurn()
    {
        discardPile.AddRange(hand);
        hand.Clear();
        DrawCards();

        AfterTurnEnd?.Invoke();
    }

    private void DrawCards()
    {
        for (int i = 0; i < 5; i++)
        {
            DrawCardOnce();
        }
    }

    private void DrawCardOnce()
    {
        if (drawPile.Count == 0)
        {
            drawPile.AddRange(discardPile);
            discardPile.Clear();
        }

        if (drawPile.Count == 0) return;

        int roll = Random.Range(0, drawPile.Count);
        CardInfo cardToDraw = drawPile[roll];
        hand.Add(cardToDraw);
        drawPile.RemoveAt(roll);
    }
}
