using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Logic.DragAndDrop
{
    public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Cube _cube;
        private DragAndDropService _dragService;

        [Inject]
        private void Construct(Cube cube, DragAndDropService dragService)
        {
            _cube = cube;
            _dragService = dragService;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragService.InvokeDrag(_cube);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragService.InvokeDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragService.InvokeEndDrag();
        }
    }
}