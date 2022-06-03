using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals.Invoices
{
    public class Invoice
    {
        public decimal Amount { get; set; }
    }

    public interface IInvoiceService
    {
        void ChangeAmount(Invoice invoice);
    }

    public class InvoicesService
    {
        private readonly IInvoiceService invoiceService;

        public InvoicesService(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        public void ChangeAmounts(List<Invoice> invoices)
        {
            foreach (var invoice in invoices)
            {
                invoiceService.ChangeAmount(invoice);
            }
        }
    }

    public class InvoiceService : IInvoiceService
    {       
        public void ChangeAmount(Invoice invoice)
        {
            invoice.Amount += 12;
        }
    }
}
