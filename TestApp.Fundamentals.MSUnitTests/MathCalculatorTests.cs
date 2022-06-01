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
            Assert.AreEqual(expected, result);
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
            float result = mathCalculator.Divide(1f, 0f);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_DecimalMianownikZero_ExpectedDivideByZeroException()
        {
            // Act
            decimal result = mathCalculator.Divide(1m, 0m);

            // Assert
        }


    }
}
