using FakeItEasy;
using System;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    // dotnet add package FakeItEasy
    public class TrackingServiceFakeItEasyTests
    {
        private readonly IFileReader fileReader;
        private readonly TrackingService trackingService;

        private const string InvalidFile = "a";

        public TrackingServiceFakeItEasyTests()
        {
            fileReader = A.Fake<IFileReader>();
            trackingService = new TrackingService(fileReader);
        }

        [Theory]
        [InlineData("{\"Latitude\":52.01,\"Longitude\":18.01}", 52.01, 18.01)]
        [InlineData("{\"Latitude\":-52.01,\"Longitude\":18.01}", -52.01, 18.01)]
        public void Get_ValidFile_ShouldReturnsLocation(string json, double lat, double lng)
        {
            // Arrange
            A.CallTo(() => fileReader.ReadAllText(A<string>.Ignored)).Returns(json);

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
            A.CallTo(() => fileReader.ReadAllText(A<string>.Ignored)).Returns(InvalidFile);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            A.CallTo(() => fileReader.ReadAllText(A<string>.Ignored)).Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
