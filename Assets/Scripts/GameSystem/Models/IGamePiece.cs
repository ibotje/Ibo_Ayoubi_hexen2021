using BoardSystem;
using System;

namespace GameSystem.Models
{
    public interface IGamePiece : IPiece
    {
        Tile Destination
        {
            get;
            set;
        }
    }
}