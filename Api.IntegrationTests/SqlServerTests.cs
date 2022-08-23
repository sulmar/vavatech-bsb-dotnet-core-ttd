using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class SqlServerTests
    {
        [Fact]
        public async Task Select_WhenCalled_ShouldReturnsValueAsync()
        {
            // Arrange
            var container = new Ductus.FluentDocker.Builders.Builder()
            .UseContainer()
            .WithHostName("localhost")
            .WithEnvironment("ACCEPT_EULA=Y", "SA_PASSWORD=<YourStrong@Passw0rd>")
            .UseImage("mcr.microsoft.com/mssql/server:2019-latest")
            .ExposePort(1533, 1433)
            .WaitForPort("1533/tcp", TimeSpan.FromSeconds(30))
            .Build()
            .Start();

            await Task.Delay(TimeSpan.FromSeconds(10));

            string connectionString = "Data Source=localhost,1533;User ID=sa;Password=<YourStrong@Passw0rd>;";
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT 1";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();

            container.Dispose();
        }

        [Fact]
        public async Task Select_UseDockerCompose_ShouldReturnsValueAsync()
        {
            // Arrange
            var container = new Ductus.FluentDocker.Builders.Builder()
               .UseContainer()
               .UseCompose()
               .FromFile("./docker-compose.yml")
               .RemoveOrphans()
               .Build()
               .Start();

            await Task.Delay(TimeSpan.FromSeconds(10));

            string connectionString = "Data Source=localhost,1533;User ID=sa;Password=<YourStrong@Passw0rd>;";
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT 1";

            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();

            container.Dispose();
        }
    }
}
