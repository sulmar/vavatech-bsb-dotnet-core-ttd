using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Mocking.UnitTests
{

    public class ReportServiceNSubsituteTests
    {
        private readonly IMessageService messageService;
        private readonly ReportService reportService;
        public ReportServiceNSubsituteTests()
        {
            messageService = Substitute.For<IMessageService>();
            reportService = new ReportService(messageService);
        }

        [Fact]
        public async Task SendSalesReportEmailAsync_Employees_ShouldSendMessage()
        {
            // Arrange
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
            await messageService.Received(3)
                .SendMessage(Arg.Any<Bot>(), Arg.Any<Employee>(),Arg.Any<string>(),Arg.Any<string>(),Arg.Any<string>());




        }

        [Fact]
        public async Task SendSalesReportEmailAsync_Employees_ShouldSendMessageCallback()
        {
            int timesCalled = 0;

            // Arrange          
            IEnumerable<Employee> employees = new List<Employee>
            {
                new Employee { Email = "a" },
                new Employee { Email = "b" },
                new Employee { Email = "c" },
                new Employee { Email = string.Empty },
                new Employee { Email = null },
            };

            messageService.SendMessage(default, default, default, default, default).ReturnsForAnyArgs(Task.CompletedTask).AndDoes(a => ++timesCalled);

            // Act
            await reportService.SendSalesReportEmailAsync(new SalesReport(), new Bot(), employees);

            // Assert
            Assert.Equal(3, timesCalled);

        }
    }
}
