using Logic.DragAndDrop;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CubeScrollInstaller : MonoInstaller
{
    [SerializeField] private Settings _settings;

    public override void InstallBindings()
    {
        Container
            .BindInstance(_settings.Image)
            .AsSingle();
        Container
            .BindInstance(_settings.RectTransform)
            .AsSingle();

        Container
            .BindInstance(_settings.CanvasGroup)
            .AsSingle();

        Container
            .BindInstance(_settings.DragHandler)
            .AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Image Image { get; private set; }

        [field: SerializeField] public RectTransform RectTransform { get; private set; }

        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        [field: SerializeField] public DragAndDropHandler DragHandler { get; private set; }
    }
}