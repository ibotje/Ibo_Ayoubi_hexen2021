using BoardSystem;
using GameSystem.Models;
using System;

namespace GameSystem.BoardQueries
{
    public abstract class BoardMovementBase
    {
        private NeighbourOffset[] _neigbourOffsets = new NeighbourOffset[] 
        { 
            new NeighbourOffset(1, 0, -1), 
            new NeighbourOffset(1, -1, 0), 
            new NeighbourOffset(0, -1, 1), 
            new NeighbourOffset(-1, 0, 1), 
            new NeighbourOffset(-1, 1, 0), 
            new NeighbourOffset(0, 1, -1) 
        };
        protected readonly Board<IGamePiece> Board;

        public BoardMovementBase(Board<IGamePiece> board)
        {
            Board = board;
        }

        public Position Offset(Position fromPosition, OffsetDir direction, int radius = 1)
        {
            NeighbourOffset neighbourOffset = _neigbourOffsets[(int)direction];
            neighbourOffset.X *= radius;
            neighbourOffset.Y *= radius;
            neighbourOffset.Z *= radius;
            return Offset(fromPosition, neighbourOffset);
        }

        private Position Offset(Position pos, NeighbourOffset offset)
        {
            return new Position(pos.X + offset.X, pos.Y + offset.Y, pos.Z + offset.Z);
        }

        protected struct NeighbourOffset
        {
            public int X;
            public int Y;
            public int Z;
            public NeighbourOffset(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public enum OffsetDir
        {
            UpRight,
            Right,
            DownRight,
            DownLeft,
            Left,
            UpLeft
        }
    }
}