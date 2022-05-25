using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public class SpatialLogicHandler<T> : ISpatialLogicHandler where T : SpatialBounds
    {
        private readonly IAreaToExplore<T> _definedArea;

        public SpatialLogicHandler(IAreaToExplore<T> definedArea)
        {
            _definedArea = definedArea;
        }

        public void DefineSpace(string initializeSpaceLine)
        {
            string[] spatialInitialData = initializeSpaceLine.Split(' ');
            int width = int.Parse(spatialInitialData[0]);
            int height = int.Parse(spatialInitialData[1]);

            _definedArea.CreateSpatialBounds(width, height);
        }
    }
}
