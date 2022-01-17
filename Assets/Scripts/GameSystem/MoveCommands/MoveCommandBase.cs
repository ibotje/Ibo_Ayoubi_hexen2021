using BoardSystem;
using System;
using System.Collections.Generic;

namespace GameSystem.MoveCommands
{
    public abstract class MoveCommandBase
    {
        public abstract void OnDropTile(Tile playerTile, Tile hoverTile);

        public abstract List<Tile> OnHoverTile(Tile playerTile, Tile hoverTile);

        public abstract bool ContainsTile(Tile playerTile, Tile hoverTile);
    }
}