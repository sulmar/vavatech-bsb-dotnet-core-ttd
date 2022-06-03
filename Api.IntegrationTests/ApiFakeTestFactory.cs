using Api.IRepositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTests
{
    public class ApiFakeTestFactory : WebApplicationFactory<Startup>
    {       
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IVehicleRepository, FakeVehicleRepository>();
            });
        }

    }
}
