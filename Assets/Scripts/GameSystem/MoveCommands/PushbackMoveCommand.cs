using BoardSystem;
using GameSystem.Models;
using GameSystem.BoardQueries;
using System;
using System.Collections.Generic;

namespace GameSystem.Models.MoveCommands
{
    [MoveCommandName("Pushback")]
    public class PushbackMoveCommand : MoveCommandBase
    {
        private readonly Board<IGamePiece> _board;
        private readonly BoardRadiusMovement _moveArea;

        public PushbackMoveCommand(Board<IGamePiece> board)
        {
            _moveArea = new BoardRadiusMovement(board);
            _board = board;
        }

        public override void OnDropTile(Tile playerTile, Tile hoverTile)
        {
            List<Tile> tiles = OnHoverTile(playerTile, hoverTile);
            Position pos = playerTile.Position;
            foreach (Tile tile in tiles)
            {
                Position pos1 = tile.Position;
                int x = pos1.X - pos.X;
                int y = pos1.Y - pos.Y;
                int z = pos1.Z - pos.Z;
                Position position = new Position(pos1.X + x, pos1.Y + y, pos1.Z + z);
                Tile tile1 = _board.TileAt(position);
                if (tile1 != null)
                    _board.Move(tile, tile1);
                else
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
            return new List<Tile>() { tiles[num1], tiles[num], tiles[num2] };
        }
        private int ModuloCalc(int number, int mod)
        {
            return (number % mod + mod) % mod;
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