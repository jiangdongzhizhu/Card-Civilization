using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardCivilization
{
    public class CardGenerator : MonoBehaviour
    {
        [SerializeField] private Transform cardRoot;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Text energyText;
        [SerializeField] private Text drawPileCountText;
        [SerializeField] private Text discardPileCountText;

        private readonly List<CardInteractor> cardInteractors = new List<CardInteractor>();

        private void Start()
        {
            CardManager.Inst.AfterInfoUpdated += UpdateCardsInfo;
        }

        private void FixedUpdate()
        {
            energyText.text = CardManager.Inst.Energy.ToString();
        }

        private void UpdateCardsInfo()
        {
            GenerateCards();
            drawPileCountText.text = CardManager.Inst.DrawPile.Count.ToString();
            discardPileCountText.text = CardManager.Inst.DiscardPile.Count.ToString();
        }

        private void GenerateCards()
        {
            while (cardInteractors.Count < CardManager.Inst.Hand.Count)
            {
                GameObject instance = Instantiate(cardPrefab, cardRoot);
                var cardInteractor = instance.GetComponentInChildren<CardInteractor>();
                cardInteractors.Add(cardInteractor);
            }

            for (int i = 0; i < cardInteractors.Count; i++)
            {
                CardInteractor cardInteractor = cardInteractors[i];
                if (i >= CardManager.Inst.Hand.Count)
                {
                    cardInteractor.gameObject.SetActive(false);
                    continue;
                }
                cardInteractor.gameObject.SetActive(true);
                cardInteractor.Initialize(CardManager.Inst.Hand[i]);
            }
        }
    }
}