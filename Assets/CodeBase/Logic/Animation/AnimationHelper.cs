using DG.Tweening;
using System;
using UnityEngine;

namespace Logic.Animation
{
    public class AnimationHelper
    {
        private const float _jumpHeight = 200f;
        private const float _jumpDuration = 0.4f;
        private const float _fallSpeed = 1300f;
        private const float _downDuration = 0.2f;
        private const float _destroyDuration = 0.3f;

        public void Jump(Cube cube, Vector2 endPos, Action onComplete = null)
        {
            cube.RectTransform.DOKill();
            cube.CanvasGroup.blocksRaycasts = false;

            cube.RectTransform
                .DORotate(new Vector3(0, 0, 90), _jumpDuration, RotateMode.LocalAxisAdd)
                .SetEase(Ease.OutQuad);

            cube.RectTransform.DOJumpAnchorPos(endPos, _jumpHeight, 1, _jumpDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => onComplete?.Invoke())
                .OnKill(() => cube.CanvasGroup.blocksRaycasts = true);
        }

        public void Fall(Cube cube, Vector2 endPos, Action onComplete = null)
        {
            cube.RectTransform.DOKill();

            cube.CanvasGroup.blocksRaycasts = false;

            float distance = Vector2.Distance(cube.Position, endPos);
            float duration = distance / _fallSpeed;

            cube.RectTransform.DORotate(new Vector3(0, 0, 180), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

            cube.RectTransform.DOAnchorPos(endPos, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() => onComplete?.Invoke())
                .OnKill(() =>
                    {
                        cube.CanvasGroup.blocksRaycasts = true;
                        cube.RectTransform.rotation = Quaternion.identity;
                    });
        }

        public void Down(Cube cube, Vector2 endPos, Action onComplete = null)
        {
            cube.RectTransform.DOKill();

            cube.CanvasGroup.blocksRaycasts = false;

            cube.RectTransform.DOAnchorPos(endPos, _downDuration)
                .OnComplete(() => onComplete?.Invoke())
                .OnKill(() => cube.CanvasGroup.blocksRaycasts = true);
        }

        public void Destroy(Cube cube, Action onComplete = null)
        {
            cube.RectTransform.DOKill();
            cube.CanvasGroup.blocksRaycasts = false;

            Sequence seq = DOTween.Sequence();
            seq.Join(cube.RectTransform.DOScale(0, _destroyDuration).SetEase(Ease.InBack));
            seq.Join(cube.CanvasGroup.DOFade(0, _destroyDuration));
            seq.OnComplete(() => onComplete?.Invoke());
            seq.OnKill(() => 
                {
                    cube.CanvasGroup.alpha = 1;
                    cube.RectTransform.localScale = Vector3.one;
                    cube.CanvasGroup.blocksRaycasts = true;
                });
        }
    }
}