using BoardSystem;
using GameSystem.Models;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class HexenPieceView : MonoBehaviour, IPiece , IGamePiece
    {
        [SerializeField]
        private PositionHelper _positionHelper;

        private HexenPiece _model;
        private Transform _boardTransform;

        public HexenPiece Model
        {
            get => _model;
            internal set
            {
                if (_model != null)
                {
                    _model.HexenPieceMoved -= ModelMoved;
                    _model.HexenPieceTaken -= ModelTaken;

                }
                _model = value;

                if (_model != null)
                {
                    _model.HexenPieceMoved += ModelMoved;
                    _model.HexenPieceTaken += ModelTaken;
                }
            }
        }

        public Tile Destination { get; set; }

        private void ModelTaken(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void ModelMoved(object sender, HexenPieceMovedEventArgs e)
        {
            var worldPosition = _positionHelper.ToWorldPosition(_boardTransform, e.To.Position);
            transform.position = worldPosition;
        }

        private void Start()
        {
            _boardTransform = FindObjectOfType<BoardView>().transform;
        }
        private void OnDestroy()
        {
            Model = null;
        }

        public void Moved(Tile fromTile, Tile toTile)
        {
        }

        public void Taken()
        {
        }
    }
}
