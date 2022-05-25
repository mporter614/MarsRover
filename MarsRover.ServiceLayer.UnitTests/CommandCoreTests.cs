using MarsRover.Shared;
using Moq;
using NUnit.Framework;
using System;

namespace MarsRover.ServiceLayer.UnitTests
{
    public class CommandCoreTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapCommandToSpecificLogicHandlerAndSendThrowsArgumentOutOfRangeExceptionIfInvalidEnumValueInputted()
        {
            // Arrange
            // Force an int outside bounds of enum through our method, should fall into default/error path of switch statement
            var fakeCommandType = (CommandType)123;
            var fakeStringLineForCommand = String.Empty;
            var spatialLogicHandlerMock = new Mock<ISpatialLogicHandler>();
            var roverLogicHandlerMock = new Mock<IRoverLogicHandler>();

            var sut = new CommandCore<RectangularSpatialBounds>(spatialLogicHandlerMock.Object, roverLogicHandlerMock.Object);
            
            // Act

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.MapCommandToSpecificLogicHandlerAndSend(fakeStringLineForCommand, fakeCommandType));
        }
    }
}