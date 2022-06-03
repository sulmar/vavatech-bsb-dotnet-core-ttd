using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Fundamentals.Invoices;
using Xunit;

namespace TestApp.Fundamentals.xUnitUnitTests
{


    public class InvoiceServiceTests
    {
        [Theory]
        [InlineData(100, 112)]
        [InlineData(1000, 1012)]
        [InlineData(200, 212)]
        public void ChangeAmount_WhenCalled_ShouldIncrementAmount(decimal amount, decimal expected)
        {
            // Arrange
            InvoiceService invoiceService = new InvoiceService();

            var invoice = new Invoices.Invoice { Amount = amount };

            // Act
            invoiceService.ChangeAmount(invoice);

            // Assert
            Assert.Equal(expected, invoice.Amount);
        }

       
    }
}
