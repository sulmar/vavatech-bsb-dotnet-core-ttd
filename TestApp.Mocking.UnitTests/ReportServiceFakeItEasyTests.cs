using FakeItEasy;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestApp.Mocking.UnitTests
{
    public class ReportServiceFakeItEasyTests
    {
        private readonly IMessageService messageService;
        private readonly ReportService reportService;
        public ReportServiceFakeItEasyTests()
        {
            messageService = A.Fake<IMessageService>();
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
            var sendMessage = A.CallTo(() => messageService.SendMessage(A<Bot>.Ignored, A<Employee>.Ignored, A<string>.Ignored, A<string>.Ignored, A<string>.Ignored));
            // Act
            await reportService.SendSalesReportEmailAsync(new SalesReport(), new Bot(), employees);

            // Assert
            sendMessage.MustHaveHappened(3, Times.Exactly);

        }
    }
}
