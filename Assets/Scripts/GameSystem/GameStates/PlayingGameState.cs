using BoardSystem;
using GameSystem.GameStates;
using GameSystem.MoveCommands;
using GameSystem.Views;
using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.GameStates
{
    class PlayingGameState : GameStateBase
    {
        private CardView _card;
        private MoveCommandBase _draggedMoveCommand;
        private List<Tile> _validTiles = new List<Tile>();
        public PlayingGameState(StateMachine<GameStateBase> stateMachine) : base(stateMachine)
        {

        }

        public override void BeginDrag(CardView cardView)
        {
            _card = cardView;
            _draggedMoveCommand = GameLoop.Instance.Deck.GetMoveCommand(cardView.Model);
        }

        public override void Dropped(Tile hoverTile)
        {
            var tile = GameLoop.Instance.Board.TileOf(GameLoop.Instance.Piece);
            if (_draggedMoveCommand.ContainsTile(tile, hoverTile))
            {
                _draggedMoveCommand.OnDropTile(tile, hoverTile);
                GameLoop.Instance.Hand.RemoveCard(_card.Model);
                GameLoop.Instance.Hand.FillHand();
            }
        }

        public override void EnteredTile(Tile hoverTile)
        {
            if (_draggedMoveCommand != null)
            {
                var tile = GameLoop.Instance.Board.TileOf(GameLoop.Instance.Piece);
                _validTiles = _draggedMoveCommand.OnHoverTile(tile, hoverTile);
                GameLoop.Instance.Board.HighLight(_validTiles);
            }
        }

        public override void ExitedTile(Tile hoverTile)
        {
            GameLoop.Instance.Board.Unhighlight(_validTiles);
            _validTiles.Clear();
        }

        public override void Ended()
        {
            StateMachine.MoveTo(GameOverState);
        }
    }
}
