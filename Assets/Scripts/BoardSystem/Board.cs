using System.Collections.Generic;
using System.Linq;

namespace BoardSystem
{
    public class Board<TPiece> where TPiece : class, IPiece
    {
        private Dictionary<Position, Tile> _tiles = new Dictionary<Position, Tile>();

        private List<TPiece> _values = new List<TPiece>();
        private List<Tile> _keys = new List<Tile>();
        private List<Tile> _points = new List<Tile>();

        public IList<Tile> Tiles => _tiles.Values.ToList();

        public TPiece Take(Tile fromTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return null;

            var piece = _values[idx];

            _values.RemoveAt(idx);
            _keys.RemoveAt(idx);

            piece.Taken();

            return piece;
        }

        public Tile Remove(Tile fromTile)
        {
            var idx = _points.IndexOf(fromTile);
            if (idx == -1)
                return null;

            var tile = _points[idx];

            _points.RemoveAt(idx);

            tile.Taken();

            return tile;
        }

        public void Move(Tile fromTile, Tile toTile)
        {
            var idx = _keys.IndexOf(fromTile);
            if (idx == -1)
                return;

            var toPiece = PieceAt(toTile);
            if (toPiece != null)
                return;

            _keys[idx] = toTile;

            var piece = _values[idx];
            piece.Moved(fromTile, toTile);
        }

        public void Place(Tile toTile, TPiece piece)
        {
            if (_keys.Contains(toTile))
                return;
            if (_values.Contains(piece))
                return;

            _keys.Add(toTile);
            _values.Add(piece);
        }

        public void Set(Tile toTile)
        {
            if (_points.Contains(toTile))
                return;

            _points.Add(toTile);
        }

        public readonly int GridSize;

        public Board(int gridSize)
        {
            GridSize = gridSize;

            initTiles();
        }

        private void initTiles()
        {
            int radius = -this.GridSize;
            int num = this.GridSize;
            for (int x = radius; x <= num; x++)
            {
                for (int y = radius; y <= num; y++)
                {
                    for (int z = radius; z <= num; z++)
                    {
                        if (x + y + z == 0) //check if valid tile
                        {
                            this.InitTile(x, y, z);
                        }
                    }
                }
            }
        }
        private void InitTile(int x, int y, int z)
        {
            Position position = new Position { X = x, Y = y, Z = z };
            Tile tile = new Tile(position);
            this._tiles.Add(position, tile);
        }

        public Tile TileAt(Position position)
        {
            Tile tile;
            if (_tiles.TryGetValue(position, out tile))
                return tile;
            return null;
        }

        public TPiece PieceAt(Tile tile)
        {
            var idx = _keys.IndexOf(tile);
            if (idx == -1)
                return default(TPiece);

            return _values[idx];
        }

        public Tile TileOf(TPiece piece)
        {
            var idx = _values.IndexOf(piece);
            if (idx == -1)
                return null;

            return _keys[idx];
        }

        public void Unhighlight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = false;
            }
        }

        public void HighLight(List<Tile> tiles)
        {
            foreach (var tile in tiles)
            {
                tile.IsHighlighted = true;
            }
        }
    }
}
