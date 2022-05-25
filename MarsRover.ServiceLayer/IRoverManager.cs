using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public interface IRoverManager<T> where T : SpatialBounds
    {
        Rover<T> CurrentRover { get; }
        void InitializeRover(int xCoordinate, int yCoordinate, CardinalDirection direction);
    }

}
