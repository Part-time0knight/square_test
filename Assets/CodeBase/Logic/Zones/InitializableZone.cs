using System;
using Zenject;

namespace Logic.Zones
{
    public abstract class InitializableZone : IZone, IInitializable
    {
        public event Action<Cube> OnSet;

        protected ZoneChecker _checker;

        public InitializableZone(ZoneChecker checker)
        {
            _checker = checker;
        }

        public virtual void Initialize()
        {
            _checker.SetZone(this);
        }

        public abstract bool Check(Cube cube);

        public virtual void Set(Cube cube)
        {
            OnSet?.Invoke(cube);
        }
    }
}