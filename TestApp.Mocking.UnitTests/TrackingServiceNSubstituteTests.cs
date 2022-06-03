using FakeItEasy;
using NSubstitute;
using System;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    // dotnet add package 
    public class TrackingServiceNSubstituteTests
    {
        private readonly IFileReader fileReader;
        private readonly TrackingService trackingService;

        private const string InvalidFile = "a";

        public TrackingServiceNSubstituteTests()
        {
            fileReader = Substitute.For<IFileReader>();
            trackingService = new TrackingService(fileReader);
        }

        [Theory]
        [InlineData("{\"Latitude\":52.01,\"Longitude\":18.01}", 52.01, 18.01)]
        [InlineData("{\"Latitude\":-52.01,\"Longitude\":18.01}", -52.01, 18.01)]
        public void Get_ValidFile_ShouldReturnsLocation(string json, double lat, double lng)
        {
            // Arrange
            // fileReader.ReadAllText(Arg.Any<string>()).Returns(json);

            // Z użyciem default dla zwiększenia czytelności
            fileReader.ReadAllText(default).ReturnsForAnyArgs(json);

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
            // fileReader.ReadAllText(Arg.Any<string>()).Returns(InvalidFile);

            // Z użyciem default dla zwiększenia czytelności
            fileReader.ReadAllText(default).ReturnsForAnyArgs(InvalidFile);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            // fileReader.ReadAllText(Arg.Any<string>()).Returns(string.Empty);

            // Z użyciem default dla zwiększenia czytelności
            fileReader.ReadAllText(default).ReturnsForAnyArgs(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
