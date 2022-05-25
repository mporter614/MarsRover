using MarsRover.Shared;

namespace MarsRover.ServiceLayer
{
    public class RoverLogicHandler<T> : IRoverLogicHandler where T : SpatialBounds
    {
        private readonly IRoverManager<T> _roverManager;

        public RoverLogicHandler(IRoverManager<T> roverManager)
        {
            _roverManager = roverManager;
        }

        //Take in the entire line commands then split up into appropriate subcommands (rover specific)
        public void InitializeRover(string initializeRoverLine)
        {
            //example line: 1 2 N
            //Create + add new rover to collection/service
            string[] roverInitialData = initializeRoverLine.Split(' ');

            //Rudimentary validation of initialize line formatting (should have 3 substrings in our current assumptions)
            if(roverInitialData.Length > 1)
            {
                var xPosition = roverInitialData[0];
                var yPosition = roverInitialData[1];
                var cardinalDirectionCurrentlyFacing = roverInitialData[2];

                _roverManager.InitializeRover(int.Parse(xPosition), int.Parse(yPosition), (CardinalDirection)Enum.Parse(typeof(CardinalDirection), cardinalDirectionCurrentlyFacing));
            }
        }

        public void MoveRover(string moveRoverLine)
        {
            //example line: LMLMLMLMM
            //Send commands down to specific rover to update its state
            for(var i = 0; i < moveRoverLine.Length; i++)
            {
                //send the individual command to the current rover
                char c = moveRoverLine[i];
                _roverManager.CurrentRover.Move((MovementCommand)Enum.Parse(typeof(MovementCommand), c.ToString()));
            }

            //Based on the command line flow, processing has stopped for current rover
            _roverManager.CurrentRover.OutputCurrentPosAndDirection();
        }
    }
}
