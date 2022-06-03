using Moq;
using System;
using Xunit;

namespace TestApp.Mocking.UnitTests
{

    // dotnet add package Moq
    public class TrackingServiceMoqTests
    {
        private readonly Mock<IFileReader> mockFileReader;
        private readonly TrackingService trackingService;

        private const string InvalidFile = "a";

        public TrackingServiceMoqTests()
        {
            mockFileReader = new Mock<IFileReader>();
            trackingService = new TrackingService(mockFileReader.Object);
        }

        [Theory]
        [InlineData("{\"Latitude\":52.01,\"Longitude\":18.01}", 52.01, 18.01)]
        [InlineData("{\"Latitude\":-52.01,\"Longitude\":18.01}", -52.01, 18.01)]
        public void Get_ValidFile_ShouldReturnsLocation(string json, double lat, double lng)
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(json);

            // Act
            var result = trackingService.Get();

            // Assert
            Assert.Equal(lat, result.Latitude);
            Assert.Equal(lng, result.Longitude);

        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowsFormatException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(InvalidFile);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.ReadAllText(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
