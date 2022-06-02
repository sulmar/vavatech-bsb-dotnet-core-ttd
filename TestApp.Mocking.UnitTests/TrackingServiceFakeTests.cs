using System;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    public class FakeValidFile : IFileReader
    {
        public string ReadAllText(string path) => "{\"Latitude\":52.01,\"Longitude\":18.01}";
    }

    public class FakeInvalidFile : IFileReader
    {
        public string ReadAllText(string path) => "a";
    }

    public class FakeEmptyFile : IFileReader
    {
        public string ReadAllText(string path) => string.Empty;
    }

    public class TrackingServiceFakeTests
    {
        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange
            IFileReader fileReader = new FakeValidFile();
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
            IFileReader fileReader = new FakeInvalidFile();
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
            IFileReader fileReader = new FakeEmptyFile();
            TrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }


    }
}
