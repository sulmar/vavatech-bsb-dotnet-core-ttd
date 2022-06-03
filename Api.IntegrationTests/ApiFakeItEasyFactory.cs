using Api.IRepositories;
using Api.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTests
{
    public class ApiFakeItEasyFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                IVehicleRepository vehicleRepository = A.Fake<IVehicleRepository>();

                A.CallTo(() => vehicleRepository.Get(1)).Returns(new Vehicle());
                A.CallTo(() => vehicleRepository.Get(0)).Returns(null);

                services.AddSingleton<IVehicleRepository>(vehicleRepository);
            });
        }
    }
}
