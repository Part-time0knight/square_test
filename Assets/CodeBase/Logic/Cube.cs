using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Logic
{
    public class Cube : MonoBehaviour
    {
        public int Index { get; set; }

        public Sprite Sprite { set => _image.sprite = value; }

        public Vector2 Position
        {
            set => _rectTransform.anchoredPosition = value;
            get => _rectTransform.anchoredPosition;
        }

        public Vector2 Size
        {
            set => _rectTransform.sizeDelta = value;
            get => _rectTransform.sizeDelta;
        }
         
        public RectTransform RectTransform { get => _rectTransform; }

        public CanvasGroup CanvasGroup { get => _canvasGroup; }

        private Image _image;

        private RectTransform _rectTransform;

        private CanvasGroup _canvasGroup;


        [Inject]
        private void Construct(Image image, RectTransform rectTransform, CanvasGroup canvasGroup)
        {
            _image = image;
            _rectTransform = rectTransform;
            _canvasGroup = canvasGroup;
        }
    }
}