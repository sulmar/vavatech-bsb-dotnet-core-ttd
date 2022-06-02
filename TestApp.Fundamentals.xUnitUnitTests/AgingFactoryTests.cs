using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    /*

     Timely,                     // <1
        ExpiredTo30Days,            // <1-30)
        ExpiredFrom30To90Days,      // <30-90)
        ExpiredFrom90To180Days,     // <90-180)
        ExpiredFrom180To360Days,    // <180-360)
        ExpiredOver360Days,         // <360, 
    */

    public class AgingFactoryTests
    {
        [Theory]
        [InlineData(0, AgingGroup.Timely)]
        [InlineData(1, AgingGroup.ExpiredTo30Days)]
        [InlineData(29, AgingGroup.ExpiredTo30Days)]

        [InlineData(30, AgingGroup.ExpiredFrom30To90Days)]
        [InlineData(89, AgingGroup.ExpiredFrom30To90Days)]

        [InlineData(90, AgingGroup.ExpiredFrom90To180Days)]
        [InlineData(179, AgingGroup.ExpiredFrom90To180Days)]

        [InlineData(180, AgingGroup.ExpiredFrom180To360Days)]        
        [InlineData(359, AgingGroup.ExpiredFrom180To360Days)]

        [InlineData(360, AgingGroup.ExpiredOver360Days)]
        public void Create_IntervalDays_ShouldReturnsTimely(int intervalDays, AgingGroup expected)
        {
            // Arrange

            // Act
            var result = AgingFactory.Create(intervalDays);

            // Assert
            Assert.Equal(expected, result);

        }
    }
}
