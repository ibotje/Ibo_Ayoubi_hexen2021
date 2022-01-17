using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName = "DefaultCardViewFactory", menuName = "GameSystem/CardView Factory")]
    public class CardViewFactory : ScriptableObject
    {
        [SerializeField]
        private List<CardView> _cardViews = new List<CardView>();

        [SerializeField]
        private List<string> _cardActionNames = new List<string>();

        public CardView CreateCardView(string card, Transform transform)
        {
            int num = _cardActionNames.IndexOf(card);
            if (num == -1)
                throw new ArgumentException($"No CardView for {card} found");
            CardView cardView = Instantiate<CardView>(_cardViews.ElementAt<CardView>(num), transform);
            cardView.Model = card;
            cardView.name = card;
            return cardView;
        }
    }
}