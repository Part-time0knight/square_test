using Logic.StaticData;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class CubeManager
    {
        public int MaxIndex { get => _cubeList.Cubes.Count; }

        private Pool _pool;
        private CubeListSO _cubeList;

        public CubeManager(Pool pool, CubeListSO cubeList)
        {
            _pool = pool;
            _cubeList = cubeList;

        }

        public virtual Cube Get(int ind)
        {
            return _pool.Spawn(ind, _cubeList.Cubes[ind]);
        }

        public virtual void Remove(Cube cube)
            => _pool.Despawn(cube);

        public class Pool : MonoMemoryPool<int, Sprite, Cube>
        {
            protected override void Reinitialize(int index, Sprite sprite, Cube item)
            {
                base.Reinitialize(index, sprite, item);
                item.Sprite = sprite;
                item.Index = index;
            }
        }
    }
}