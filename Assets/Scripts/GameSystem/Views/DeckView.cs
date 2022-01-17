using CardSystem;
using GameSystem.Models.MoveCommands;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField]
        private CardViewFactory _cardViewFactory;
        private Hand<MoveCommandBase> _model;

        private List<string> _cards = new List<string>();

        private List<CardView> _cardViews = new List<CardView>();

        public Hand<MoveCommandBase> Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != null)
                {
                    _model.CardAdded -= new EventHandler<CardEventArgs>(this.OnCardAdded);
                    _model.CardRemoved -= new EventHandler<CardEventArgs>(this.OnCardRemoved);
                }
                _model = value;
                if (_model != null)
                {
                    InitCardViews();
                    _model.CardAdded += new EventHandler<CardEventArgs>(this.OnCardAdded);
                    _model.CardRemoved += new EventHandler<CardEventArgs>(this.OnCardRemoved);
                }
            }
        }

        private void InitCardView(string card)
        {
            CardView cardView = _cardViewFactory.CreateCardView(card, base.transform);
            cardView.transform.SetParent(base.transform);
            cardView.name = String.Concat("Card ( ", card, ")");
            _cards.Add(card);
            _cardViews.Add(cardView);
        }

        private void InitCardViews()
        {
            foreach (string card in _model.Cards)
            {
                InitCardView(card);
            }
        }

        private void OnCardAdded(object sender, CardEventArgs e)
        {
            InitCardView(e.Card);
        }

        private void OnCardRemoved(object sender, CardEventArgs e)
        {
            int num = _cards.IndexOf(e.Card);
            CardView item = _cardViews[num];
            _cards.RemoveAt(num);
            _cardViews.RemoveAt(num);
            item.Remove();
        }


    }
}