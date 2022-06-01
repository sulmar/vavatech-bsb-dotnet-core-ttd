using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestApp.Fundamentals.MSUnitTests
{
    [TestClass]
    public class MathCalculatorTests
    {
        private MathCalculator mathCalculator;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            mathCalculator = new MathCalculator();
        }

        // Method_Scenario_ExpectedBehavior
        
        [DataTestMethod]
        [DataRow(1, 2, 3)]
        [DataRow(2, 1, 3)]
        [DataRow(0, 0, 0)]
        public void Add_WhenCalled_ReturnsTheSumOfArguments(int a, int b, int expected)
        {
            // Act
            int result = mathCalculator.Add(a, b);

            // Assert
            // Assert.AreEqual(expected, result);

            // dotnet add package FluentAssertions
            result.Should().Be(expected);
        }



        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Add_MaxArguments_ExpectedOverflowException()
        {
            // Act
            mathCalculator.Add(int.MaxValue, int.MaxValue);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_IntMianownikZero_ExpectedDivideByZeroException()
        {
            // Act
            mathCalculator.Divide(1, 0);

            // Assert
        }

        [TestMethod]       
        public void Divide_IntMianownikZero_ExpectedDivideByZeroException2()
        {
            // Act
            Action act = () => mathCalculator.Divide(1, 0);

            // Assert
            Assert.ThrowsException<DivideByZeroException>(act);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_FloatMianownikZero_ExpectedDivideByZeroException()
        {
            // Act
            mathCalculator.Divide(1f, 0f);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_DecimalMianownikZero_ExpectedDivideByZeroException()
        {
            // Act
            mathCalculator.Divide(1m, 0m);

            // Assert
        }

        [TestMethod]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            int result = mathCalculator.Max(2, 1);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(1, 2);

            // Assert

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Max_FirstAndSecondArgumentIsEqual_ReturnsFirstArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(1, 1);

            // Assert

            Assert.AreEqual(1, result);
        }

        [DataTestMethod]
        [DataRow(2, 1, 2, DisplayName = "FirstArgumentIsGreater")]
        [DataRow(1, 2, 2, DisplayName = "SecondArgumentIsGreater")]
        [DataRow(1, 1, 1, DisplayName = "FirstAndSecondArgumentIsEqual")]
        public void Max_ValidArguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            int result = mathCalculator.Max(first, second);

            // Assert
            Assert.AreEqual(expected, result);
        }


    }
}
