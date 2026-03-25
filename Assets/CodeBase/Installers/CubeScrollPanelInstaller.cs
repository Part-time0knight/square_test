using Logic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers
{
    public class CubeScrollPanelInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_settings.ScrollRect)
                .AsSingle();

            Container
                .BindInstance(_settings.ContentLayout)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CubeScrollPanel>()
                .FromInstance(_settings.CubeScroll)
                .AsSingle()
                .NonLazy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField]
            public ScrollRect ScrollRect { get; private set; }

            [field: SerializeField]
            public CubeScrollPanel CubeScroll { get; private set; }

            [field: SerializeField]
            public HorizontalLayoutGroup ContentLayout { get; private set; }
        }
    }
}