
namespace Logic.Zones
{
    public interface IZone
    {
        bool Check(Cube cube);

        void Set(Cube cube);
    }
}