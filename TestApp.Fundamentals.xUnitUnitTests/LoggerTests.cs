using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class LoggerTests
    {
        private Logger logger;

        public LoggerTests()
        {
            logger = new Logger();
        }

        [Fact]
        public void Log_EmptyMessage_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => logger.Log(string.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

      

        [Fact]
        public void Log_ValidMessage_ShouldSetLastMessage()
        {
            // Arrange

            // Act
            logger.Log("a");

            // Assert
            Assert.Equal("a", logger.LastMessage);
        }


        [Fact]
        public async Task LogAsync_ValidMessage_ShouldSetLastMessage()
        {
            // Act
            await logger.LogAsync("a");

            // Assert
            logger.LastMessage.Should().Be("a");
        }

        [Fact]
        public void Log_ValidMessage_ShouldRaiseMessageLoggedEvent()
        {
            // Arrange            
            using var monitoredSubject = logger.Monitor();

            // Act
            logger.Log("a");

            // Assert
            monitoredSubject.Should().Raise(nameof(Logger.MessageLogged));
        }

            /*

            [Fact]
            public void Log_ValidMessage_ShouldRaiseMessageLoggedEvent()
            {            
                var raisedEvent = Assert.Raises<DateTime>(  // Arrange
                    a => logger.MessageLogged += a,
                    a => logger.MessageLogged -= a,
                    () => logger.Log("a") // Act
                    );

                // Assert
                Assert.NotNull(raisedEvent);
            }

            */
        }

}
