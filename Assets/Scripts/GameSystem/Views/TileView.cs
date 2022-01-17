using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    [SelectionBase]
    public class TileView : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
    {
        private Tile _model;

        [SerializeField]
        private PositionHelper _positionHelper;

        [SerializeField]
        private Material _highlightMaterial;

        private Material _originalMaterial;
        private MeshRenderer _meshRenderer;

        internal float Size
        {
            set
            {

            }
        }

        public Tile Model
        {
            get => _model;
            set
            {
                if (_model != null)
                {
                    _model.HighlightStatusChanged -= ModelHighLightStatusChanged;
                    _model.TileTaken -= ModelTaken;
                }


                _model = value;
                if (_model != null)
                {
                    _model.HighlightStatusChanged += ModelHighLightStatusChanged;
                    _model.TileTaken += ModelTaken;
                }

            }
        }

        private void Start()
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
            _originalMaterial = _meshRenderer.sharedMaterial;

            GameLoop.Instance.Initalized += OnGameInitialized;
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            var board = GameLoop.Instance.Board;
            var boardPosition = _positionHelper.ToBoardPosition(transform.position);
            var tile = board.TileAt(boardPosition);

            Model = tile;
        }

        private void ModelHighLightStatusChanged(object sender, EventArgs e)
        {
            if (Model.IsHighlighted)
                _meshRenderer.material = _highlightMaterial;
            else
                _meshRenderer.material = _originalMaterial;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameLoop.Instance.OnEnteredTile(_model);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameLoop.Instance.OnExitedTile(_model);
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameLoop.Instance.OnDropped(_model);
        }

        public void Taken()
        {

        }
        private void ModelTaken(object sender, EventArgs e)
        {
            Destroy(this.gameObject);
        }

        private void OnDestroy()
        {
            Model = null;
        }
    }
}
