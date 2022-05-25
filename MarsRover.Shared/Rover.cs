namespace MarsRover.Shared
{
    public class Rover<T> : IVehicle where T : SpatialBounds
    {
        private readonly IAreaToExplore<T> _definedArea;

        public Rover(IAreaToExplore<T> definedArea)
        {
            _definedArea = definedArea;
        }

        public Position2DPlane Position { get; set; }
        public CardinalDirection CardinalDirection { get; set; }

        public void Move(MovementCommand command)
        {
            var bounds = _definedArea.SpatialBounds as RectangularSpatialBounds;
            switch (command)
            {
                case MovementCommand.L:
                case MovementCommand.R:
                    CardinalDirection = GetNextDirectionAsAResultOfTurn(CardinalDirection, command);
                    break;
                case MovementCommand.M:
                    // Handling staying within bounds with light validations here
                    if (CardinalDirection == CardinalDirection.N)
                    {
                        if(Position.PositionY < bounds?.Height)
                        {
                            Position.PositionY++;
                        }
                    }
                    if (CardinalDirection == CardinalDirection.E)
                    {
                        if (Position.PositionX < bounds?.Width)
                        {
                            Position.PositionX++;
                        }
                    }
                    if (CardinalDirection == CardinalDirection.S)
                    {
                        if (Position.PositionY > 0)
                        {
                            Position.PositionY--;
                        }
                    }
                    if (CardinalDirection == CardinalDirection.W)
                    {
                        if (Position.PositionX > 0)
                        {
                            Position.PositionX--;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(command.ToString());
            }
        }

        public void OutputCurrentPosAndDirection()
        {
            Console.WriteLine($"{Position.PositionX} {Position.PositionY} {CardinalDirection}");
        }

        //This is a finite state machine realistically but just spinning up a quick and dirty method to map these transitions when turning
        private static CardinalDirection GetNextDirectionAsAResultOfTurn(CardinalDirection currentDirection, MovementCommand turnCommand)
        {
            if(turnCommand == MovementCommand.L)
            {
                if(currentDirection == CardinalDirection.N)
                {
                    return CardinalDirection.W;
                }
                else if (currentDirection == CardinalDirection.W)
                {
                    return CardinalDirection.S;
                }
                else if (currentDirection == CardinalDirection.S)
                {
                    return CardinalDirection.E;
                }
                else if (currentDirection == CardinalDirection.E)
                {
                    return CardinalDirection.N;
                }
            }
            else if(turnCommand == MovementCommand.R)
            {
                if (currentDirection == CardinalDirection.N)
                {
                    return CardinalDirection.E;
                }
                else if (currentDirection == CardinalDirection.E)
                {
                    return CardinalDirection.S;
                }
                else if (currentDirection == CardinalDirection.S)
                {
                    return CardinalDirection.W;
                }
                else if (currentDirection == CardinalDirection.W)
                {
                    return CardinalDirection.N;
                }
            }
            //Error/invalid command path - not focusing on bolstering up this method just yet since its a private method fairly deep into logic
            return currentDirection;
        }
    }
}
