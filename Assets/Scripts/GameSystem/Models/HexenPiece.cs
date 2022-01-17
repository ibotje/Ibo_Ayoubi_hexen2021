using BoardSystem;
using System;
using UnityEngine;

namespace GameSystem.Models
{
    public class HexenPieceMovedEventArgs : EventArgs
    {
        public Tile From { get; }
        public Tile To { get; }
        public HexenPieceMovedEventArgs(Tile from, Tile to)
        {
            From = from;
            To = to;
        }
    }

    public class HexenPiece : IPiece, IGamePiece
    {
        public event EventHandler<HexenPieceMovedEventArgs> HexenPieceMoved;
        public event EventHandler HexenPieceTaken;

        public Tile Destination
        {
            get;
            set;
        }

        public HexenPiece()
        {

        }

        void IPiece.Moved(Tile fromTile, Tile toTile)
        {
            OnHexenPieceMoved(new HexenPieceMovedEventArgs(fromTile, toTile));
        }

        void IPiece.Taken()
        {
            OnHexenPieceTaken(EventArgs.Empty);
        }

        protected virtual void OnHexenPieceMoved(HexenPieceMovedEventArgs arg)
        {
            EventHandler<HexenPieceMovedEventArgs> handler = HexenPieceMoved;
            handler?.Invoke(this, arg);
        }

        protected virtual void OnHexenPieceTaken(EventArgs arg)
        {
            EventHandler handler = HexenPieceTaken;
            handler?.Invoke(this, arg);
        }
    }
}
