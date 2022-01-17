using System;

namespace CardSystem
{
    public class CardEventArgs : EventArgs
    {
        public string Card
        {
            get;
        }

        public CardEventArgs(string card)
        {
            Card = card;
        }
    }
}