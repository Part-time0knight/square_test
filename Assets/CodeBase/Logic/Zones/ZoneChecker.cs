using Logic.DragAndDrop;
using System.Collections.Generic;
using Zenject;

namespace Logic.Zones
{
    public class ZoneChecker : IInitializable
    {
        public IZone OutOfZones { set => _outOfZones = value; }

        private List<IZone> _zones = new();
        private IZone _outOfZones;
        private DragAndDropService _dragService;

        public ZoneChecker(DragAndDropService dragService) 
        {
            _dragService = dragService;
        }

        public void SetZone(IZone zone)
            => _zones.Add(zone);

        public void Initialize()
        {
            _dragService.OnEndDrag += SetCube;
        }

        private void SetCube(Cube cube)
        {
            foreach (var zone in _zones)
            {
                bool check = zone.Check(cube);
                if (check) 
                {
                    zone.Set(cube);
                    return;
                }
            }

            _outOfZones.Set(cube);
        }
    }
}