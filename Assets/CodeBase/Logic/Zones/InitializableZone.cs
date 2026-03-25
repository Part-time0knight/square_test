using Zenject;

namespace Logic.Zones
{
    public abstract class InitializableZone : IZone, IInitializable
    {
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

        public abstract void Set(Cube cube);
    }
}