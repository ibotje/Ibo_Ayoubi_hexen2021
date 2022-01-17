using BoardSystem;
using GameSystem.Models;
using GameSystem.BoardQueries;
using System;
using System.Collections.Generic;

namespace GameSystem.Models.MoveCommands
{
    [MoveCommandName("SwipeAttack")]
    public class SwipeAttackMoveCommand : MoveCommandBase
    {
        private readonly Board<IGamePiece> _board;
        private readonly BoardRadiusMovement _moveArea;

        public SwipeAttackMoveCommand(Board<IGamePiece> board)
        {
            _moveArea = new BoardRadiusMovement(board);
            _board = board;
        }

        public override void OnDropTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            foreach (Tile tile in tiles)
            {
                _board.Take(tile);
            }
        }

        public override List<Tile> OnHoverTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = _moveArea.Radius(playerTile, 1);
            if (!tiles.Contains(hoverTile))
                return tiles;
            int num = tiles.IndexOf(hoverTile);
            int num1 = ModuloCalc(num - 1, tiles.Count - 1);
            int num2 = ModuloCalc(num + 1, tiles.Count - 1);
            return new List<Tile>()
            {tiles[num1],tiles[num],tiles[num2]};
        }
        private int ModuloCalc(int number, int mod)
        {
            return (number % mod + mod) % mod;
        }

        public override bool ContainsTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = _moveArea.Radius(playerTile, 1);
            if (!tiles.Contains(hoverTile))
                return false;
            return true;
        }
    }
}