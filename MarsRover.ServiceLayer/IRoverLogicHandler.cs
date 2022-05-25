namespace MarsRover.ServiceLayer
{
    public interface IRoverLogicHandler
    {
        void InitializeRover(string initializeRoverLine);
        void MoveRover(string moveRoverLine);
    }

}
