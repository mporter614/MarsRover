using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public class CommandCore<T> : ICommandCore where T : SpatialBounds
    {
        private readonly ISpatialLogicHandler _spatialLogicHandler;
        private readonly IRoverLogicHandler _roverLogicHandler;

        public CommandCore(ISpatialLogicHandler spatialLogicHandler, IRoverLogicHandler roverLogicHandler)
        {
            _spatialLogicHandler = spatialLogicHandler;
            _roverLogicHandler = roverLogicHandler;
        }

        public void MapCommandToSpecificLogicHandlerAndSend(string commandContent, CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.AreaDefinition:
                    _spatialLogicHandler.DefineSpace(commandContent);
                    break;
                case CommandType.RoverInitialization:
                    _roverLogicHandler.InitializeRover(commandContent);
                    break;
                case CommandType.RoverMovement:
                    _roverLogicHandler.MoveRover(commandContent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(commandType.ToString());
            }
        }
    }
}
