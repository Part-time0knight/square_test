using Logic.DragAndDrop;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic
{
    public class CubeScrollPanel : MonoBehaviour, IInitializable
    {
        private ScrollRect _scroll;
        private CubeManager _manager;
        private DragAndDropService _dragService;
        private List<Cube> _cubes = new();

        [Inject]
        private void Construct(CubeManager manager, ScrollRect scroll,
            DragAndDropService dragService)
        {
            _manager = manager;
            _scroll = scroll;
            _dragService = dragService;
        }

        public void Initialize()
        {
            int maxIndex = _manager.MaxIndex;

            for (int i = 0; i < maxIndex; i++)
            {
                AddCube(i);
            }
            _dragService.OnBeginDrag += InvokeBeginDrag;
        }

        private void InvokeBeginDrag(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;

            _cubes.Remove(cube);
            AddCube(cube.Index);
        }

        private void AddCube(int index)
        {
            Cube cube = _manager.Get(index);
            cube.transform.SetParent(_scroll.content, false);
            _cubes.Add(cube);
            cube.transform.SetSiblingIndex(index);
        }
    }
}