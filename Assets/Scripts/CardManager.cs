using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CardCivilization
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager Inst { get; private set; }

        private List<Skill> drawPile = new List<Skill>();
        private List<Skill> hand = new List<Skill>();
        private List<Skill> discardPile = new List<Skill>();

        public IReadOnlyList<Skill> DrawPile => drawPile;
        public IReadOnlyList<Skill> Hand => hand;
        public IReadOnlyList<Skill> DiscardPile => discardPile;
        public int EnergyRestore { get; set; } = 3;
        public int Energy { get; private set; }

        public event Action OnTurnEnd;
        public event Action AfterInfoUpdated;

        private void Awake()
        {
            Inst = this;

            drawPile.AddRange(new Skill[] {
                new GrowerCard(),
                new GiftGiverCard(),
                new EquivalentLoverCard(),
                new BigNumLoverCard(),
                new RequestSupportCard(),
                new RetrieveCard()
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

        public bool CanSelectCard(Skill skill)
        {
            return skill.Cost <= Energy;
        }

        public void PlayCard(Skill skill)
        {
            Energy -= skill.Cost;
            hand.Remove(skill);
            if (!skill.IsExhausted)
            {
                discardPile.Add(skill);
            }

            AfterInfoUpdated?.Invoke();
        }

        public void AddCardToHand<T>(int num = 1) where T : Skill, new()
        {
            for (int i = 0; i < num; i++)
            {
                hand.Add(new T());
            }

            AfterInfoUpdated?.Invoke();
        }

        public void DrawCards(int num = 1)
        {
            for (int i = 0; i < num; i++)
            {
                DrawCardOnce();
            }

            AfterInfoUpdated?.Invoke();
        }

        public void EndTurn()
        {
            if (Energy < EnergyRestore)
            {
                Energy = EnergyRestore;
            }
            discardPile.AddRange(hand);
            hand.Clear();
            OnTurnEnd?.Invoke();
            DrawCardsOnTurnEnd();

            AfterInfoUpdated?.Invoke();
        }

        private void DrawCardsOnTurnEnd()
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
            var cardToDraw = drawPile[roll];
            hand.Add(cardToDraw);
            drawPile.RemoveAt(roll);
        }
    }
}