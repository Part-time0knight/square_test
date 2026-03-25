using Logic.Animation;
using System;
using Zenject;

namespace Logic.Zones
{
    public class OutOfZones : IZone, IInitializable
    {
        private CubeManager _cubeManager;
        protected ZoneChecker _checker;
        private AnimationHelper _animationHelper;

        public OutOfZones(CubeManager cubeManager,
            ZoneChecker checker)
        { 
            _cubeManager = cubeManager;
            _checker = checker;
            _animationHelper = new AnimationHelper();
        }

        public void Initialize()
        {
            _checker.OutOfZones = this;
        }

        public bool Check(Cube cube)
            => true;


        public void Set(Cube cube)
        {
            Action callback = () => _cubeManager.Remove(cube);
            _animationHelper.Destroy(cube, callback);
        }
    }
}