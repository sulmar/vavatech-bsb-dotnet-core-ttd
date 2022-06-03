using Api.IRepositories;
using Api.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Api.IntegrationTests
{
    public class ApiNSubstituteFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
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
