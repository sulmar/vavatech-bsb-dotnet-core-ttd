using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    public class SalaryCalculatorServiceTests
    {
        [Theory]
        [InlineData(1f, 100f)]
        [InlineData(4f, 400f)]
        [InlineData(4.5f, 450f)]
        public async Task CalculateAsync_AmountPLN_ShouldReturnsAmountByCurrencyRatio(float ratio, decimal expected)
        {
            // Arrange         
            SalaryCalculatorService salaryCalculatorService = new SalaryCalculatorService();

            // Act
            var result = await salaryCalculatorService.CalculateAsync(100, ratio);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
