using BoardSystem;
using GameSystem.Models;
using GameSystem.BoardQueries;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    [MoveCommandName("ForwardAttack")]
    public class ForwardAttackMoveCommand : MoveCommandBase
    {
        private readonly Board<IGamePiece> _board;

        private readonly BoardLineMovement _moveArea;

        public ForwardAttackMoveCommand(Board<IGamePiece> board)
        {
            _moveArea = new BoardLineMovement(board);
            _board = board;
        }

        public override void OnDropTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            foreach (Tile tile in tiles)
            {
                if (_board.PieceAt(tile) == null)
                    continue;
                _board.Take(tile);
            }
        }

        public override List<Tile> OnHoverTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = new List<Tile>();
            BoardMovementBase.OffsetDir[] values = (BoardMovementBase.OffsetDir[])Enum.GetValues(typeof(BoardMovementBase.OffsetDir));
            for (int i = 0; i < (int)values.Length; i++)
            {
                BoardMovementBase.OffsetDir offsetDirection = values[i];
                List<Tile> tiles1 = _moveArea.Line(playerTile, offsetDirection);
                if (tiles1.Contains(hoverTile))
                    return tiles1;
                tiles.AddRange(tiles1);
            }
            return tiles;
        }

        public override bool ContainsTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            if (!tiles.Contains(hoverTile))
                return false;
            return true;
        }
    }
}