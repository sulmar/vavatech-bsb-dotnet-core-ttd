using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    // dotnet add package Moq
    public class TrackingServiceMockTests
    {
        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();
            Mock<ILogger<TrackingService>> mockLogger = new Mock<ILogger<TrackingService>>();

            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("{\"Latitude\":52.01,\"Longitude\":18.01}");

            IFileReader fileReader = mockFileReader.Object;

            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            var result = trackingService.Get();

            // Assert
            Assert.Equal(52.01, result.Latitude);
            Assert.Equal(18.01, result.Longitude);

        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowsFormatException()
        {
            // Arrange
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();
            Mock<ILogger<TrackingService>> mockLogger = new Mock<ILogger<TrackingService>>();


            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns("a");

            IFileReader fileReader = mockFileReader.Object;

            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            Mock<IFileReader> mockFileReader = new Mock<IFileReader>();
            Mock<ILogger<TrackingService>> mockLogger = new Mock<ILogger<TrackingService>>();

            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(string.Empty);

            IFileReader fileReader = mockFileReader.Object;

            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
