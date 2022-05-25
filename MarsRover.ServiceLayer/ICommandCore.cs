using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public interface ICommandCore
    {
        void MapCommandToSpecificLogicHandlerAndSend(string commandContent, CommandType commandType);
    }
}
