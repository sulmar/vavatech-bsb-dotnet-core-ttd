using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals.MSUnitTests
{
    [TestClass]
    public class LoggerTests
    {
        private Logger logger;

        [TestInitialize]
        public void Setup()
        {
            logger = new Logger();
        }

        // Method_scenario_expectedbehavior
        [TestMethod]
        public void Log_EmptyMessage_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => logger.Log(string.Empty);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(act);
        }

        [TestMethod]
        public void Log_ValidMessage_ShouldSetLastMessage()
        {
            // Arrange

            // Act
            logger.Log("a");

            // Assert
            Assert.AreEqual("a",logger.LastMessage);
        }

        [TestMethod]
        public void Log_ValidMessage_ShouldRaiseMessageLoggedEvent()
        {
            // Arrange
            DateTime logDate = DateTime.MinValue;
            logger.MessageLogged += (sender, args) => logDate = args;

            // Act
            logger.Log("a");

            // Assert
            Assert.AreNotEqual(DateTime.MinValue, logDate);
        }

        [TestMethod]
        public void Log_ValidMessage_ShouldRaiseMessageLoggedEvent2()
        {
            // Arrange
            DateTime? logDate = null;
            logger.MessageLogged += (sender, args) => logDate = args;

            // Act
            logger.Log("a");

            // Assert
            Assert.IsTrue(logDate.HasValue);
        }

        [TestMethod]
        public void Log_ValidMessage_ShouldRaiseStartEvent()
        {
            // Arrange
            bool isRaised = false;
            logger.Started += (sender, args) => isRaised = true;

            // Act
            logger.Log("a");

            // Assert
            Assert.IsTrue(isRaised);
        }


    }
}
