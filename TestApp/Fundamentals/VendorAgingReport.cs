using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Fundamentals
{

    // Raport wiekowania płatności
    // na podst. https://inforfk.pl/cykle-tematyczne/tresc,inforfk,,INF0000000000000696389,Jak-wiekowac-naleznosci-handlowe-w-ksiegach-rachunkowych.html
    public class VendorAgingReport
    {
        public DateTime CreateDate { get; private set; }
        public string Title { get; private set; }
        public IDictionary<AgingGroup, decimal> Details { get; private set; }

        public VendorAgingReport(string title)
        {
            Title = title;
            CreateDate = DateTime.Now;

            Details = new Dictionary<AgingGroup, decimal>
            {
                { AgingGroup.Timely, 0 },
                { AgingGroup.ExpiredTo30Days, 0 },
                { AgingGroup.ExpiredFrom30To90Days, 0 },
                { AgingGroup.ExpiredFrom90To180Days, 0 },
                { AgingGroup.ExpiredFrom180To360Days, 0 },
                { AgingGroup.ExpiredOver360Days, 0 },
            };
        }
    }

    public class AgingFactory
    {
        public static AgingGroup Create(int intervalDays)
        {
            if (intervalDays < 1)
            {
                return AgingGroup.Timely;
            }
            else if (intervalDays >= 1 && intervalDays < 30)
            {
                return AgingGroup.ExpiredTo30Days;
            }
            else if (intervalDays >= 30 && intervalDays < 90)
            {
                return AgingGroup.ExpiredFrom30To90Days;
            }
            else if (intervalDays >= 90 && intervalDays < 180)
            {
                return AgingGroup.ExpiredFrom90To180Days;
            }
            else if (intervalDays >= 180 && intervalDays < 360)
            {
                return AgingGroup.ExpiredFrom180To360Days;
            }
            else
            {
                return AgingGroup.ExpiredOver360Days;
            }
        }
    }

    public class VendorAgingCalculator
    {
       

        public VendorAgingReport Calculate(params Invoice[] invoices)
        {
            VendorAgingReport vendorAgingReport = new VendorAgingReport($"Raport wiekowania płatności na dzień {DateTime.Today:dd.MM.yyyy}");

            foreach (var invoice in invoices)
            {
                var intervalDays = (invoice.DueDate - DateTime.Now).TotalDays;

                var agingGroup = AgingFactory.Create((int) intervalDays);

                vendorAgingReport.Details[agingGroup] += invoice.TotalAmount;
                
            }


            return vendorAgingReport;

        }


    }

    public abstract class Base
    {

    }

    public class Vendor : Base
    {
        public string Name { get; set; }
    }

    public class Invoice : Base
    {
        public DateTime DueDate { get; set; }
        public Vendor Vendor { get; set; }
        public decimal TotalAmount { get; set; }
    }

    /// <summary>
    /// Struktura wiekowa należności
    /// </summary>
    public enum AgingGroup
    {
        Timely,                     // <1
        ExpiredTo30Days,            // 1-30
        ExpiredFrom30To90Days,      // 31-90
        ExpiredFrom90To180Days,     // 91-180
        ExpiredFrom180To360Days,    // 181-360
        ExpiredOver360Days,         // >360
    }


}
