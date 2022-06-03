using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    #region Models

    public class Order
    {
        public DateTime OrderedDate { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }

        public List<OrderDetail> Details { get; set; }

        public decimal Total => Details.Sum(d => d.Total);
    }

    public enum OrderStatus
    {
        Boxing,
        Sent,
    }

    public class OrderDetail
    {
        public OrderDetail(decimal unitPrice, short quantity = 1)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }

    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class Employee : User
    {
        public bool IsBoss { get; set; }
    }

    public class Bot : User
    {

    }

    public abstract class Report
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; }

        public Report()
        {
            CreatedOn = DateTime.Now;
        }
    }

    public class SalesReport : Report
    {
        public TimeSpan TotalTime { get; set; }

        public decimal TotalAmount { get; set; }

        public string Title { get; set; }


        public override string ToString()
        {
            return $"Report created on {CreatedOn} \r\n TotalAmount: {TotalAmount}";
        }

        public string ToHtml()
        {
            return $"<html>Report created on <b>{CreatedOn}</b> <p>TotalAmount: {TotalAmount}<p></html>";
        }
    }

    #endregion

    public interface IMessageService
    {
        Task SendMessage(Bot sender, Employee recipient, string subject, string content, string htmlContent);
    }

    public class SendGridMessageService : IMessageService
    {
        private const string apikey = "your_secret_key";

        private readonly ISendGridClient client;
        private readonly ILogger<SendGridMessageService> logger;

        public delegate void ReportSentHandler(object sender, ReportSentEventArgs e);
        public event ReportSentHandler ReportSent;

        public SendGridMessageService(ISendGridClient sendGridClient, ILogger<SendGridMessageService> logger)
        {
            // dotnet add package SendGrid
            this.client = sendGridClient;
            this.logger = logger;
        }
        

        public async Task SendMessage(Bot sender, Employee recipient, string subject, string content, string htmlContent)
        {
            var message = MailHelper.CreateSingleEmail(
                   new EmailAddress(sender.Email, $"{sender.FirstName} {sender.LastName}"),
                   new EmailAddress(recipient.Email, $"{recipient.FirstName} {recipient.LastName}"),
                   "Raport sprzedaży",
                   content,
                   htmlContent);

            var response = await client.SendEmailAsync(message);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                ReportSent?.Invoke(this, new ReportSentEventArgs(DateTime.Now));

                logger.LogInformation($"Raport został wysłany.");
            }
            else
            {
                logger.LogInformation($"Błąd podczas wysyłania raportu.");

                throw new ApplicationException("Błąd podczas wysyłania raportu.");
            }
        }
    }

    public interface IUserRepository
    {
        IEnumerable<Employee> GetEmployees(bool isBoss);
        IEnumerable<Employee> GetRecipients();
        Bot GetBot();
    }

    public class DbUserRepository : IUserRepository
    {
        private readonly SalesContext salesContext;

        public DbUserRepository(SalesContext salesContext)
        {
            this.salesContext = salesContext;
        }

        public Bot GetBot()
        {
            return salesContext.Users.OfType<Bot>().Single();
        }

        public IEnumerable<Employee> GetRecipients()
        {
            return GetEmployees(true).Where(e => !string.IsNullOrEmpty(e.Email));
        }

        public IEnumerable<Employee> GetEmployees(bool isBoss)
        {
            return salesContext.Users.OfType<Employee>().Where(e => e.IsBoss == isBoss).ToList();
        }
    }


    public class SalesReportBuilder
    {
        private SalesReport salesReport = new SalesReport();
        private IEnumerable<Order> orders;

        public void AddOrders(IEnumerable<Order> orders)
        {
            this.orders = orders;
        }

        public void AddTotalAmount()
        {
            salesReport.TotalAmount = orders.Sum(o => o.Total);
        }

        public SalesReport Build()
        {                        
            return salesReport;
        }
    }



    public class ReportService
    {
        private readonly IMessageService messageService;

        public ReportService(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task SendSalesReportEmailAsync(SalesReport report, Bot sender, IEnumerable<Employee> recipients)
        {                                   
            foreach (var recipient in recipients)
            {
                if (recipient.Email == null)
                    continue;

                await messageService.SendMessage(sender, recipient, report.Title, report.ToString(), report.ToHtml());

              //  Logger.Info($"Wysyłanie raportu do {recipient.FirstName} {recipient.LastName} <{recipient.Email}>...");             
              
            }
        }

       
    }

    public class ReportSentEventArgs : EventArgs
    {
        public readonly DateTime SentDate;

        public ReportSentEventArgs(DateTime sentDate)
        {
            this.SentDate = sentDate;
        }
    }

    public class OrderService
    {
        private readonly SalesContext context;

        public OrderService()
        {
            context = new SalesContext();
        }

        public IEnumerable<Order> Get(DateTime from, DateTime to)
        {
            return context.Orders.Where(o => o.OrderedDate > from && o.OrderedDate < to).ToList();
        }
    }

    public class SalesContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }


   

}