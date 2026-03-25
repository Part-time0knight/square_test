
using Logic.Animation;
using UnityEngine;

namespace Logic.Zones
{
    public class HoleZone : InitializableZone
    {
        private const int _maskOffset = 25;

        private RectTransform _maskRect;
        private RectTransform _holePanel;
        private RectTransform _imageRect;
        private Canvas _canvas;
        private AnimationHelper _animationHelper;
        private CubeManager _cubeManager;

        public HoleZone(RectTransform holePanel, 
            RectTransform mask,
            RectTransform imageTransform,
            Canvas canvas,
            ZoneChecker checker,
            CubeManager cubeManager) : base(checker)
        {
            _maskRect = mask;
            _holePanel = holePanel;
            _canvas = canvas;
            _imageRect = imageTransform;
            _animationHelper = new AnimationHelper();
            _cubeManager = cubeManager;
        }

        public override bool Check(Cube cube)
        {
            Camera camera = _canvas.worldCamera;
            Vector2 leftPos = cube.RectTransform.TransformPoint(-1f * new Vector2(cube.Size.x / 2f, 0f));
            Vector2 rightPos = cube.RectTransform.TransformPoint(new Vector2(cube.Size.x / 2f, 0f));
            
            Vector2 screenPointLeft = RectTransformUtility
                .WorldToScreenPoint(camera, leftPos);

            Vector2 screenPointRight = RectTransformUtility
                .WorldToScreenPoint(camera, rightPos);

            return RectTransformUtility
                .RectangleContainsScreenPoint(_maskRect, screenPointLeft, camera)
                || RectTransformUtility
                .RectangleContainsScreenPoint(_maskRect, screenPointRight, camera);
        }

        public override void Set(Cube cube)
        {
            cube.transform.SetParent(_holePanel);
            if (!CheckCollisionToImage(cube))
            {
                Vector2 offset = GetNearPointToDrop(cube);
                _animationHelper.Jump(cube, cube.Position + offset, () => Fall(cube));
                return;
            }
            Fall(cube);
        }

        private void Fall(Cube cube)
        {
            cube.transform.SetParent(_maskRect);

            _animationHelper.Fall(cube,
                new(cube.Position.x,  -(_maskRect.sizeDelta.y + cube.Size.y)),
                () => _cubeManager.Remove(cube));
        }

        private bool CheckCollisionToImage(Cube cube)
        {

            Camera camera = _canvas.worldCamera;
            Vector2 leftCubePos = cube.RectTransform.TransformPoint(new Vector2(-cube.Size.x / 2f, 0f));
            Vector2 rightCubePos = cube.RectTransform.TransformPoint(new Vector2(cube.Size.x / 2f, 0f));
            Vector2 BotCubePos = cube.RectTransform.TransformPoint(new Vector2(0f, -cube.Size.y / 2f));

            Vector2 screenPointLeft = RectTransformUtility
                .WorldToScreenPoint(camera, leftCubePos);

            Vector2 screenPointRight = RectTransformUtility
                .WorldToScreenPoint(camera, rightCubePos);


            bool checkBottom = _imageRect.position.y < BotCubePos.y;
            bool checkBorders = RectTransformUtility
                .RectangleContainsScreenPoint(_maskRect, screenPointLeft, camera) 
                && RectTransformUtility
                .RectangleContainsScreenPoint(_maskRect, screenPointRight, camera);

            return checkBottom && checkBorders || checkBorders;
        }

        private Vector2 GetNearPointToDrop(Cube cube)
        {
            Vector2 result = cube.transform.position;
            Vector2 BotCubePos = cube.RectTransform.TransformPoint(new Vector2(0f, -cube.Size.y / 2f));
            bool checkBottom = _imageRect.position.y < BotCubePos.y;
            if (!checkBottom)
                result = new(result.x, result.y + _imageRect.position.y - BotCubePos.y);

            Vector2 maskLeft = _maskRect.TransformPoint(new Vector2(-_maskRect.sizeDelta.x / 2 + _maskOffset, 0f));
            Vector2 maskRight = _maskRect.TransformPoint(new Vector2(_maskRect.sizeDelta.x / 2 - _maskOffset, 0f));

            Vector2 leftCubePos = cube.RectTransform.TransformPoint(new Vector2(-cube.Size.x / 2f, 0f));
            Vector2 rightCubePos = cube.RectTransform.TransformPoint(new Vector2(cube.Size.x / 2f, 0f));

            if (maskLeft.x > leftCubePos.x)
                result = new(result.x + maskLeft.x - leftCubePos.x, result.y);

            if (maskRight.x < rightCubePos.x)
                result = new(result.x + maskRight.x - rightCubePos.x, result.y);

            result = cube.RectTransform.InverseTransformPoint(result);
            return result;
        }
    }
}