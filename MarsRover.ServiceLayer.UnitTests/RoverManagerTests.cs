using MarsRover.Shared;
using Moq;
using NUnit.Framework;
using System;

namespace MarsRover.ServiceLayer.UnitTests
{
    public class RoverManagerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitializeRoverThrowsArgumentExceptionIfInitializedOutsideSpatialBounds()
        {
            // Arrange
            var mockSpace = new Mock<IAreaToExplore<RectangularSpatialBounds>>();
            var spatialBounds = new RectangularSpatialBounds() { Width = 4, Height = 2 };
            mockSpace.Setup(m => m.SpatialBounds).Returns(spatialBounds);

            var sut = new RoverManager<RectangularSpatialBounds>(mockSpace.Object);

            // Act

            // Assert
            Assert.Throws<ArgumentException>(() => sut.InitializeRover(15, 1, CardinalDirection.N));
        }
    }
}