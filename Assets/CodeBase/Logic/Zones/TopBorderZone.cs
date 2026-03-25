using Logic.Animation;
using System;
using UnityEngine;

namespace Logic.Zones
{
    public class TopBorderZone : InitializableZone
    {
        private RectTransform _borderRect;
        private Canvas _canvas;
        private CubeManager _cubeManager;
        private AnimationHelper _animationHelper;

        public TopBorderZone(RectTransform borderRect, 
            Canvas canvas,
            CubeManager cubeManager,
            ZoneChecker checker) : base(checker)
        {
            _borderRect = borderRect;
            _canvas = canvas;
            _cubeManager = cubeManager;
            _animationHelper = new();
        }

        public override bool Check(Cube cube)
        {
            Camera camera = _canvas.worldCamera;
            Vector2 screenPoint = RectTransformUtility
                .WorldToScreenPoint(camera, cube.transform.position);
            return RectTransformUtility
                .RectangleContainsScreenPoint(_borderRect, screenPoint, camera);
        }

        public override void Set(Cube cube)
        {
            base.Set(cube);
            Action callback = () => _cubeManager.Remove(cube);
            _animationHelper.Destroy(cube, callback);
        }
    }
}