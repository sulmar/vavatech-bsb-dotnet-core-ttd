using Api.IRepositories;
using Api.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace Api.IntegrationTests
{
    public class ApiNSubstituteFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                services.AddTransient(typeof(ILogger<>), typeof(NullLogger<>)); // Rejestracja minimalistycznego loggera, który nic nie robi
            });


            builder.ConfigureTestServices(services =>
            {
                IVehicleRepository vehicleRepository = Substitute.For<IVehicleRepository>();

                vehicleRepository.Get(1).Returns(new Vehicle());
                vehicleRepository.Get(0).Returns(default(Vehicle));

                services.AddSingleton<IVehicleRepository>(vehicleRepository);
            });
        }
    }
}
