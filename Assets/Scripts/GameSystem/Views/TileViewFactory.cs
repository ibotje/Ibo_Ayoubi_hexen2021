using BoardSystem;
using UnityEngine;

namespace GameSystem.Views
{
    [CreateAssetMenu(fileName ="DefaultTileViewFactory", menuName="GameSystem/TileView Factory")]
    public class TileViewFactory : ScriptableObject
    {
        [SerializeField]
        private TileView _TileView;

        [SerializeField]
        private PositionHelper _positionHelper;

        public TileView CreateTileView<TPiece>(Board<TPiece> board, Tile tile, Transform parent) where TPiece : class ,IPiece
        {
            var position = _positionHelper.ToLocalPosition(tile.Position);
            var prefab = _TileView;

            var tileView = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);

            tileView.Size = this._positionHelper.TileSize;
            tileView.name = $"Tile{tile.Position.X},{tile.Position.Y},{tile.Position.Z}";
            //Link the view with the model
            tileView.Model = tile;

            return tileView;
        }
    }
}
