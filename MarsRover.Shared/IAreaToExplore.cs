namespace MarsRover.Shared
{
    public interface IAreaToExplore<T> where T : SpatialBounds
    {
        T SpatialBounds { get; }
        void CreateSpatialBounds(int x, int y);
    }
}
