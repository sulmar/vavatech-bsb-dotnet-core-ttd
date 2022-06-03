using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    // dotnet add package Microsoft.AspNetCore.TestHost

    public class TestStartup : Startup
    {
        public Mock<IVehicleRepository> mockVehicleRepository;

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            mockVehicleRepository = new Mock<IVehicleRepository>(MockBehavior.Strict);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton(mockVehicleRepository);

            base.ConfigureServices(services);


            mockVehicleRepository
                .Setup(vr => vr.Get(1))
                .Returns(new Models.Vehicle());

            mockVehicleRepository
                .Setup(vr => vr.Get(0))
                .Returns<Vehicle>(null);


            services.AddSingleton<IVehicleRepository>( mockVehicleRepository.Object);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class VehiclesControllerTests
    {
        private readonly HttpClient client;

        public VehiclesControllerTests()
        {
            TestServer server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartup>()
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
