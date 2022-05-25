namespace MarsRover.Shared
{
    public class Plateau : IAreaToExplore<RectangularSpatialBounds>
    {
        public RectangularSpatialBounds SpatialBounds { get; private set; }

        public void CreateSpatialBounds(int x, int y)
        {
            SpatialBounds = new RectangularSpatialBounds() { Width = x, Height = y};
        }
    }
}
