using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystem.Views
{
    public class CardView : MonoBehaviour, IDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler
    {
        public string Model
        {
            get;
            internal set;
        }

        private GameObject _draggable;

        private RectTransform _canvas;

        private void DestroyDraggable()
        {
            if (_draggable != null)
                Destroy(_draggable);
        }

        private Canvas FindCanvas(GameObject gameObject)
        {
            Canvas canvas = gameObject.GetComponentInParent<Canvas>();
            if (canvas != null)
                return canvas;
            for (Transform i = gameObject.transform.parent; i != null && canvas == null; i = i.parent)
            {
                canvas = i.gameObject.GetComponent<Canvas>();
            }
            return canvas;
        }

        private void FindDraggingPlane()
        {
            Canvas canva = FindCanvas(gameObject);
            if (canva == null)
                return;
            _canvas = canva.transform as RectTransform;
        }

        private void MoveDraggable(PointerEventData eventData)
        {
            Vector3 position;
            RectTransform component = _draggable.GetComponent<RectTransform>();
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, eventData.position + new Vector2(0f, -75f), eventData.pressEventCamera, out position))
            {
                component.position = position;
                component.rotation = _canvas.rotation;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            FindDraggingPlane();
            _draggable = Instantiate(gameObject, _canvas);
            MoveDraggable(eventData);
            GameLoop.Instance.OnCardDragBegin(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveDraggable(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DestroyDraggable();
        }

        public void Remove()
        {
            DestroyDraggable();
            Destroy(gameObject);
        }
    }
}