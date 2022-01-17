using BoardSystem;
using GameSystem.Models;
using System;
using System.Collections.Generic;

namespace GameSystem.BoardQueries
{
    public class BoardRadiusMovement : BoardMovementBase
    {
        public BoardRadiusMovement(Board<IGamePiece> board) : base(board)
        {
        }

        public List<Tile> Radius(Tile fromTile, int radius)
        {
            List<Tile> tiles = new List<Tile>();
            Position pos = Offset(fromTile.Position, OffsetDir.Left, radius);
            Tile tile = Board.TileAt(pos);
            if (tile != null)
                tiles.Add(tile);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    pos = Offset(pos, (OffsetDir)i, 1);
                    tile = Board.TileAt(pos);
                    if (tile != null)
                        tiles.Add(tile);
                }
            }
            return tiles;
        }
    }
}