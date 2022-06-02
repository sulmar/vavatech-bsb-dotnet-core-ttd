using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Mocking
{
     
    public interface IRateService
    {
        Task<Rate> GetAsync(string currencyCode);
    }

    public class NbpRateService : IRateService
    {
        const string url = "api/exchangerates/tables/a/?format=json";

        public async Task<Rate> GetAsync(string currencyCode)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.nbp.pl/");

            var rates = await client.GetFromJsonAsync<RatesList[]>(url);
            
            Rate rate = rates.SelectMany(p => p.rates).SingleOrDefault(r => r.code == currencyCode);

            return rate;

        }
    }

    public class NbpPageRateService : IRateService
    {
        public Task<Rate> GetAsync(string currencyCode)
        {
            throw new NotImplementedException();
        }
    }

    public class SalaryCalculatorService
    {
        public async Task<decimal> CalculateAsync(decimal amount, float ratio)
        {
            decimal result = amount * (decimal)ratio;

            return result;

        }
    }


    public class RatesList
    {
        public string table { get; set; }
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public Rate[] rates { get; set; }
    }

    public class Rate
    {
        public string currency { get; set; }
        public string code { get; set; }
        public float mid { get; set; }
    }
}
