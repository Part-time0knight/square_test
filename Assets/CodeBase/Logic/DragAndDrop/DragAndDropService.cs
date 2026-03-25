using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.DragAndDrop
{
    public class DragAndDropService
    {
        public event Action<Cube> OnBeginDrag;
        public event Action<Cube> OnDrag;
        public event Action<Cube> OnEndDrag;

        private Cube _cubeOnDrag;

        private RectTransform _dragContainer;

        public DragAndDropService(RectTransform dragContainer) 
        {
            _dragContainer = dragContainer;
        }

        public void InvokeDrag(Cube cube)
        {
            _cubeOnDrag = cube;
            _cubeOnDrag.transform.SetParent(_dragContainer);
            _cubeOnDrag.transform.rotation = Quaternion.identity;
            OnBeginDrag?.Invoke(_cubeOnDrag);
        }

        public void InvokeDrag(PointerEventData eventData)
        {
            RectTransformUtility
                .ScreenPointToLocalPointInRectangle(_dragContainer,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 localPoint);
            _cubeOnDrag.Position = localPoint;
            OnDrag?.Invoke(_cubeOnDrag);
        }

        public void InvokeEndDrag()
        {
            Cube cube = _cubeOnDrag;
            _cubeOnDrag = null;
            OnEndDrag?.Invoke(cube);

        }
    }
}