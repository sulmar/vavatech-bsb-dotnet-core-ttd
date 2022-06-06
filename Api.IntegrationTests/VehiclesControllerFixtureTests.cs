using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class VehiclesControllerFixtureTests : IClassFixture<ApiNSubstituteFactory>
    {
        private readonly HttpClient client;

        public VehiclesControllerFixtureTests(ApiMoqFactory factory)
        {
            client = factory.CreateClient();
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
