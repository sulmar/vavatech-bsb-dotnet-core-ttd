using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals.Gus;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class ReportFactoryTests
    {
        [Fact]
        public void Create_TypeIsF_ShouldReturnsSoleTraderReport()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("F");

            // Assert
            Assert.IsType<SoleTraderReport>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Theory]
        [InlineData("P")]
        [InlineData("LP")]
        [InlineData("LF")]
        public void Create_TypeIsPOrLPOrLF_ShouldReturnsLegalPersonalityReport(string type)
        {
            // Act
            var result = ReportFactory.Create(type);

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }

        [Fact]
        public void Create_TypeIsUnknown_ShouldThrowsNotSupportedException()
        {
            // Act
            Action act = () => ReportFactory.Create("a");

            // Assert
            Assert.Throws<NotSupportedException>(act);
        }


    }
}
