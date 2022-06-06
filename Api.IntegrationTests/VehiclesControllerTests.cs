using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class VehiclesControllerTests
    {
        private readonly HttpClient client;

        public VehiclesControllerTests()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.AddTransient(typeof(ILogger<>), typeof(NullLogger<>)); // Rejestracja minimalistycznego loggera, kt�ry nic nie robi
                })
                .ConfigureTestServices(services =>
                {
                    Mock<IVehicleRepository> mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);

                    mockVehicleRepository
                        .Setup(vr => vr.Get(1))
                        .Returns(new Vehicle());

                    mockVehicleRepository
                        .Setup(vr => vr.Get(0))
                        .Returns<Vehicle>(null);
                    
                    services.AddSingleton<IVehicleRepository>(mockVehicleRepository.Object);
                })
                .UseEnvironment("Development"));

            client = server.CreateClient();
        }

        [Fact]
        public async Task Get_ExistsId_ShouldReturnsOkAndContent()
        {
            // Arrange            

            // Act
            var response = await client.GetAsync("api/vehicles/1");

            var json = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(json);
        }

        [Fact]
        public async Task Get_NotExistId_ShouldReturnsNotFound()
        {
            // Arrange

            // Act
            var response = await client.GetAsync("api/vehicles/0");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
