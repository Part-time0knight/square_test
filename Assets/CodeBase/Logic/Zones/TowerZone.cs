using Logic.Animation;
using Logic.DragAndDrop;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Zones
{
    public class TowerZone : InitializableZone
    {
        private RectTransform _zoneRect;
        private Canvas _canvas;
        private List<Cube> _cubes = new List<Cube>();
        private Vector2 _bottomPos;
        private Vector2 _topPos;
        private Vector2 _towerRange;
        private DragAndDropService _dragService;
        private AnimationHelper _animationHelper;

        public TowerZone(RectTransform zoneTransform,
            ZoneChecker checker,
            Canvas canvas,
            DragAndDropService dragAndDropService) : base(checker)
        {
            _zoneRect = zoneTransform;
            _canvas = canvas;
            _dragService = dragAndDropService;
            _animationHelper = new();
        }

        public override void Initialize()
        {
            base.Initialize();
            _dragService.OnBeginDrag += InvokeDrag;
        }

        public override bool Check(Cube cube)
        {
            if (_cubes.Count == 0)
                return ZoneCheck(cube);

            return ZoneCheck(cube) && TowerZoneCheck(cube);
        }

        public override void Set(Cube cube)
        {
            base.Set(cube);
            cube.transform.SetParent(_zoneRect);
            if (_cubes.Count == 0)
            {
                SetBaseCube(cube);
                return;
            }
            SetCube(cube);
        }

        private void InvokeDrag(Cube cube)
        {
            if (!_cubes.Contains(cube)) return;
            int index = _cubes.FindIndex(c => c == cube);
            _cubes.RemoveAt(index);

            if (_cubes.Count == 0) return;
            Cube last = _cubes.Last();
            if (_cubes.Count == index)
            {
                _topPos = new(_bottomPos.x, last.Position.y);
                return;
            }
            
            for (int i = index; i < _cubes.Count; i++)
            {
                _animationHelper.Down(_cubes[i],
                    new(_cubes[i].Position.x, _cubes[i].Position.y - _cubes[i].Size.y));
            }
        }

        private bool ZoneCheck(Cube cube)
        {
            Camera camera = _canvas.worldCamera;
            Vector2 screenPoint = RectTransformUtility
                .WorldToScreenPoint(camera, cube.transform.position);
            return RectTransformUtility
                .RectangleContainsScreenPoint(_zoneRect, screenPoint, camera);
        }

        private bool TowerZoneCheck(Cube cube)
        {
            Camera camera = _canvas.worldCamera;
            Vector2 screenPoint = RectTransformUtility
                .WorldToScreenPoint(camera, cube.transform.position);

            if (camera.pixelHeight < screenPoint.y)
                return false;

            Cube lastCube = _cubes.Last();

            Vector2 screenPointBase = RectTransformUtility
                .WorldToScreenPoint(camera, lastCube.transform.position);

            if (screenPoint.y < (screenPointBase.y + lastCube.Size.y/2))
                return false;

            if (screenPoint.x < (screenPointBase.x + _towerRange.x * 2)
                || screenPoint.x > (screenPointBase.x + _towerRange.y * 2))
                return false;

            return true;
        }



        private void SetBaseCube(Cube cube)
        {
            _bottomPos = cube.Position;
            _topPos = cube.Position;
            _towerRange = new (cube.Size.x / -2, cube.Size.x / 2);
            _cubes.Add(cube);
        }

        private void SetCube(Cube cube)
        {
            float offset = Random.Range(_towerRange.x, _towerRange.y);
            Cube lastCube = _cubes.Last();
            Vector2 pos = new(_bottomPos.x + offset, _topPos.y + lastCube.Size.y);
            _cubes.Add(cube);
            _animationHelper.Jump(cube, pos);
            _topPos = new(_bottomPos.x, pos.y);
        }
    }
}