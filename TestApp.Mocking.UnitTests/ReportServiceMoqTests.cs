using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Mocking.UnitTests
{

    public class ReportServiceMoqTests
    {
        [Fact]
        public async Task SendSalesReportEmailAsync_Employees_ShouldSendMessage()
        {
            // Arrange
            Mock<IMessageService> mockMessageService = new Mock<IMessageService>();
            mockMessageService
                .Setup(ms => ms.SendMessage(It.IsAny<Bot>(), It.IsAny<Employee>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            ReportService reportService = new ReportService(mockMessageService.Object);

            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Email = "a" },
                new Employee { Email = "b" },
                new Employee { Email = "c" },
                new Employee { Email = string.Empty },
                new Employee { Email = null },
            };

            // Act
            await reportService.SendSalesReportEmailAsync(new SalesReport(), new Bot(), employees);

            // Assert
            mockMessageService.Verify(ms => ms.SendMessage(
                It.IsAny<Bot>(),
                It.IsAny<Employee>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Exactly(3));

        }

        [Fact]
        public async Task SendSalesReportEmailAsync_Employees_ShouldSendMessage2()
        {
            int timesCalled = 0;

            // Arrange
            Mock<IMessageService> mockMessageService = new Mock<IMessageService>();

            mockMessageService
                .Setup(ms => ms.SendMessage(It.IsAny<Bot>(), It.IsAny<Employee>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback( () => ++timesCalled);

            ReportService reportService = new ReportService(mockMessageService.Object);

            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Email = "a" },
                new Employee { Email = "b" },
                new Employee { Email = "c" },
                new Employee { Email = string.Empty },
                new Employee { Email = null },
            };

            // Act
            await reportService.SendSalesReportEmailAsync(new SalesReport(), new Bot(), employees);

            // Assert
            Assert.Equal(3, timesCalled);

        }
    }
}
