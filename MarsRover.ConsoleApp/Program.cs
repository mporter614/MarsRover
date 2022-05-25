using MarsRover.ServiceLayer;
using MarsRover.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //A little messy unfortunately on my generics / class structure to try and make the domain/space definition extensible
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ISpatialLogicHandler, SpatialLogicHandler<RectangularSpatialBounds>>()
                .AddSingleton<IRoverLogicHandler, RoverLogicHandler<RectangularSpatialBounds>>()
                .AddSingleton<IAreaToExplore<RectangularSpatialBounds>, Plateau>()
                .AddSingleton<IRoverManager<RectangularSpatialBounds>, RoverManager<RectangularSpatialBounds>>()
                .BuildServiceProvider();

            var spatialLogicHandlerReference = serviceProvider.GetService<ISpatialLogicHandler>();
            var roverLogicHandlerReference = serviceProvider.GetService<IRoverLogicHandler>();

            var commandCore = new CommandCore<RectangularSpatialBounds>(spatialLogicHandlerReference, roverLogicHandlerReference);

            //Default testing data, attempting to read file from filesystem based on path given as arg[0]
            var argsDefaultsForTestingDummyData = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
            var fileContentFromArgs = String.Empty;

            try
            {
                fileContentFromArgs = args.Length > 0 ? File.ReadAllText(args[0]) : "";
                //A quirk I found with the text file I quickly spun up, had to trim out return character
                fileContentFromArgs = fileContentFromArgs.Replace("\r", String.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in reading text from file specified in arguments: {ex}");
                Console.WriteLine("Falling back to dummy data within Program.cs");
            }

            string[]? linesOfInput = fileContentFromArgs.Length > 0 ? fileContentFromArgs.Split("\n") : argsDefaultsForTestingDummyData.Split("\n");

            //Begin inputting the commands from the string data
            //First line is always defining the space
            commandCore.MapCommandToSpecificLogicHandlerAndSend(linesOfInput[0], CommandType.AreaDefinition);

            //After space definition, pairs of input lines defining rovers then moving them
            for(int i = 1; i < linesOfInput.Length; i++)
            {
                //Every odd index line will be a Rover initialize line
                if(i % 2 == 1)
                {
                    commandCore.MapCommandToSpecificLogicHandlerAndSend(linesOfInput[i], CommandType.RoverInitialization);
                }
                //Every even index line will be a Rover movement line
                else
                {
                    commandCore.MapCommandToSpecificLogicHandlerAndSend(linesOfInput[i], CommandType.RoverMovement);
                }
            }

            Console.ReadKey();
        }
    }
}
