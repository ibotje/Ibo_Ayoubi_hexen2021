using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;

namespace GameSystem.BoardQueries
{
    public class BoardLineMovement : BoardMovementBase
    {
        public BoardLineMovement(Board<IGamePiece> board) : base(board)
        {
        }

        public List<Tile> Line(Tile fromTile, OffsetDir dir)
        {
            List<Tile> tiles = new List<Tile>();
            Position pos = Neighbour(fromTile.Position, dir);
            for (Tile i = Board.TileAt(pos); i != null; i = Board.TileAt(pos))
            {
                tiles.Add(i);
                pos = Neighbour(pos, dir);
            }
            return tiles;
        }

        protected Position Neighbour(Position pos, OffsetDir dir)
        {
            return Offset(pos, dir, 1);
        }
    }
}