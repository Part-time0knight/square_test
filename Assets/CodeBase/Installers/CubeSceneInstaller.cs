using Logic;
using Logic.DragAndDrop;
using Logic.Zones;
using System;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CubeSceneInstaller : MonoInstaller
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings()
        {
            InstallManagers();
            InstallDragAndDrop();
            InstallZones();
        }

        private void InstallManagers()
        {
            Container
                .BindMemoryPool<Cube, CubeManager.Pool>()
                .FromComponentInNewPrefab(_settings.Manager.CubePrefab)
                .UnderTransform(_settings.Manager.CubeContainer);

            Container
                .BindInterfacesAndSelfTo<CubeManager>()
                .AsSingle()
                .NonLazy();
        }

        private void InstallDragAndDrop()
        {
            Container
                .BindInterfacesAndSelfTo<DragAndDropService>()
                .AsSingle()
                .WithArguments(_settings.DragAndDrop.DragContainer)
                .NonLazy();
        }

        private void InstallZones()
        {
            Container
                .BindInterfacesAndSelfTo<ZoneChecker>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<OutOfZones>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<TowerZone>()
                .AsSingle()
                .WithArguments(_settings.Zones.TowerRect, _settings.Zones.DragCanvas)
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<HoleZone>()
                .AsSingle()
                .WithArguments(_settings.Zones.HoleRect,
                    _settings.Zones.MaskRect,
                    _settings.Zones.HoleImageRect,
                    _settings.Zones.DragCanvas)
                .NonLazy();
        }

        [Serializable]
        public class Settings
        {
            [field: SerializeField]
            public SettingsManager Manager { get; private set; }

            [field: SerializeField]
            public SettingsDragAndDrop DragAndDrop { get; private set; }

            [field: SerializeField]
            public SettingsZones Zones { get; private set;}

            [Serializable]
            public class SettingsManager
            {
                [field: SerializeField]
                public GameObject CubePrefab { get; private set; }

                [field: SerializeField]
                public Transform CubeContainer { get; private set; }
            }

            [Serializable]
            public class SettingsDragAndDrop
            {
                [field: SerializeField]
                public Transform DragContainer { get; private set; }
            }

            [Serializable]
            public class SettingsZones
            {
                [field: SerializeField]
                public RectTransform TowerRect { get; private set; }

                [field: SerializeField]
                public Canvas DragCanvas { get; private set; }

                [field: SerializeField]
                public RectTransform HoleRect { get; private set; }

                [field: SerializeField]
                public RectTransform HoleImageRect { get; private set; }

                [field: SerializeField]
                public RectTransform MaskRect { get; private set; }

            }

        }
    }
}