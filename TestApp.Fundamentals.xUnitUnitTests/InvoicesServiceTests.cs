using Moq;
using System.Collections.Generic;
using TestApp.Fundamentals.Invoices;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{
    public class InvoicesServiceTests
    {
        [Fact]
        public void ChangeAmount_WhenCalled_ShouldCalledIncrementAmount()
        {
            // Arrange
            Mock<IInvoiceService> mockInvoiceService = new Mock<IInvoiceService>();

            mockInvoiceService
                .Setup(s => s.ChangeAmount(It.IsAny<Invoices.Invoice>()));

            InvoicesService invoicesService = new InvoicesService(mockInvoiceService.Object);

            var invoices = new List<Invoices.Invoice>
            {
                new Invoices.Invoice(),
                new Invoices.Invoice(),
                new Invoices.Invoice(),
            };

            // Act
            invoicesService.ChangeAmounts(invoices);

            // Assert
            mockInvoiceService.Verify(s => s.ChangeAmount(It.IsAny<Invoices.Invoice>()), Times.Exactly(3));
        }
    }
}
