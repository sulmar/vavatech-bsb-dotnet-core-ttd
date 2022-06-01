using FluentAssertions;
using System;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class MathCalculatorTests
    {
        private MathCalculator mathCalculator;

        public MathCalculatorTests()
        {
            mathCalculator = new MathCalculator();
        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Arrange

            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            // Assert.Equal(2, result);

            // dotnet add package FluentAssertions
            result.Should().Be(2);

        }

        [Fact]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(1, 2);

            // Assert
            //Assert.Equal(2, result);
            result.Should().Be(2);
        }

        [Fact]
        public void Max_FirstAndSecondArgumentIsEqual_ReturnsFirstArgument()
        {
            // Arrange

            // Act
            int result = mathCalculator.Max(1, 1);

            // Assert
            //Assert.Equal(1, result);
            result.Should().Be(1);
        }


        [Fact]
        public void Divide_IntMianownikZero_ExpectedDivideByZeroException2()
        {
            // Act
            Action act = () => mathCalculator.Divide(1, 0);

            // Assert
            //Assert.Throws<DivideByZeroException>(act);

            act.Should().Throw<DivideByZeroException>();
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(0, 0, 0)]
        public void Add_WhenCalled_ReturnsTheSumOfArguments(int a, int b, int expected)
        {
            // Act
            int result = mathCalculator.Add(a, b);

            // Assert
            //Assert.Equal(expected, result);

            result.Should().Be(expected);            
        }
    }
}
