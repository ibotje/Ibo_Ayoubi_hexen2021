using System;
using System.Collections.Generic;
using System.Linq;

namespace CardSystem
{
    public class Deck<TCardAction>
    {
        private List<string> _cards = new List<string>();

        private Random _random = new Random();

        private Dictionary<string, TCardAction> _moveCommands = new Dictionary<string, TCardAction>();

        public List<string> Cards
        {
            get
            {
                return _cards;
            }
        }

        public void AddCard(string card, int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                _cards.Add(card);
            }
        }

        public Hand<TCardAction> CreateHand(int maxCards)
        {
            return new Hand<TCardAction>(this, maxCards);
        }

        public TCardAction GetMoveCommand(string card)
        {
            if (!_moveCommands.ContainsKey(card))
                return default(TCardAction);
            return _moveCommands[card];
        }

        public void RegisterMoveCommand(string card, TCardAction cardAction)
        {
            if (_moveCommands.ContainsKey(card))
                return;
            _moveCommands.Add(card, cardAction);
        }

        public bool DrawCard(out string card)
        {
            card = null;
            if (_cards.Count == 0)
                return false;
            int num = _random.Next(_cards.Count);
            card = _cards.ElementAt<string>(num);
            _cards.RemoveAt(num);
            return true;
        }
    }
}