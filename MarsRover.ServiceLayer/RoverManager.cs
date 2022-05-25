using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public class RoverManager<T> : IRoverManager<T> where T : SpatialBounds
    {
        private readonly IAreaToExplore<T> _definedArea;

        public RoverManager(IAreaToExplore<T> definedArea)
        {
            _definedArea = definedArea;
        }

        public List<Rover<T>> Rovers { get; } = new List<Rover<T>>();

        //Since we are not directing commands at specific rovers by command line, going to add current rover for convenience
        public Rover<T> CurrentRover { get; private set; }

        public void InitializeRover(int xCoordinate, int yCoordinate, CardinalDirection direction)
        {
            var bounds = _definedArea.SpatialBounds as RectangularSpatialBounds;
            if(xCoordinate > bounds?.Width || yCoordinate > bounds?.Height)
            {
                throw new ArgumentException("Coordinates for rover are out of bounds");
            }

            var roverToAdd = new Rover<T>(_definedArea)
            {
                CardinalDirection = direction,
                Position = new Position2DPlane() { PositionX = xCoordinate, PositionY = yCoordinate }
            };
            Rovers.Add(roverToAdd);
            CurrentRover = roverToAdd;
        }
    }
}
