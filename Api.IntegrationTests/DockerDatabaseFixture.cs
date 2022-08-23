using Ductus.FluentDocker.Services;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class DockerDatabaseFixture : IAsyncLifetime
    {
        private const string BaseConnectionString = @"Data Source=localhost,1533;User ID=sa;Password=<YourStrong@Passw0rd>;";
        ICompositeService container;

        public async Task InitializeAsync()
        {
            container = new Ductus.FluentDocker.Builders.Builder()
              .UseContainer()
              .UseCompose()
              .FromFile("./docker-compose.yml")
              .RemoveOrphans()
              .Build()
              .Start();

            using var connection = new SqlConnection(BaseConnectionString);
            await connection.OpenAsync();
        }

        public Task DisposeAsync()
        {
            container.Dispose();

            return Task.CompletedTask;
        }

       
    }
}
