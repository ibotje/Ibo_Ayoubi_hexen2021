using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    [MoveCommandName("Teleport")]
    public class TeleportMoveCommand : MoveCommandBase
    {
        private readonly Board<IGamePiece> _board;

        public TeleportMoveCommand(Board<IGamePiece> board)
        {
            _board = board;
        }

        public override void OnDropTile(Tile playerTile, Tile hoverTile)
        {
            if (OnHoverTile(playerTile, hoverTile).Contains(hoverTile))
            {
                _board.Move(playerTile, hoverTile);
            }
        }

        public override List<Tile> OnHoverTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = new List<Tile>();
            if (_board.PieceAt(hoverTile) == null)
            {
                tiles.Add(hoverTile);
            }
            return tiles;
        }
        public override bool ContainsTile(Tile playerTile, Tile hoverTile)
        {
            return true;
        }
    }
}