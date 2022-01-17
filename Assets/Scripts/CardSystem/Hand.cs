using System;
using System.Collections.Generic;

namespace CardSystem
{
    public class Hand<TCardAction>
    {
        private readonly List<string> _cards = new List<string>();
        private readonly Deck<TCardAction> _deck;
        private readonly int _maxCardCount;

        public List<string> Cards
        {
            get
            {
                return _cards;
            }
        }

        internal Hand(Deck<TCardAction> deck, int maxCards)
        {
            _deck = deck;
            _maxCardCount = maxCards;
            FillHand();
        }

        public void FillHand()
        {
            string card;
            for (int i = _cards.Count; i < _maxCardCount; i++)
            {
                if (_deck.DrawCard(out card))
                {
                    _cards.Add(card);
                    OnCardAdded(new CardEventArgs(card));
                }
            }
        }

        protected virtual void OnCardAdded(CardEventArgs args)
        {
            EventHandler<CardEventArgs> cardAddedHandler = CardAdded;
            if (cardAddedHandler == null)
                return;
            cardAddedHandler(this, args);
        }

        protected virtual void OnCardRemoved(CardEventArgs args)
        {
            EventHandler<CardEventArgs> cardRemovedHandler = CardRemoved;
            if (cardRemovedHandler == null)
                return;
            cardRemovedHandler(this, args);
        }

        public void RemoveCard(string card)
        {
            _cards.Remove(card);
            OnCardRemoved(new CardEventArgs(card));
        }

        public event EventHandler<CardEventArgs> CardAdded;
        public event EventHandler<CardEventArgs> CardRemoved;
    }
}