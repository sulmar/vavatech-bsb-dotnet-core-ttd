using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{

    public class DatabaseFixture : IAsyncLifetime
    {
        private const string BaseConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;";

        private readonly string _dbName;

        public string ConnectionString { get; }

        public DatabaseFixture()
        {
            _dbName = $"test-db-{Guid.NewGuid()}";

            var builder = new SqlConnectionStringBuilder(BaseConnectionString)
            {
                InitialCatalog = _dbName
            };

            ConnectionString = builder.ToString();
        }

        public async Task InitializeAsync()
        {
            using var connection = new SqlConnection(BaseConnectionString);
            await connection.OpenAsync();

            await ExecuteDbCommandAsync(connection, $"CREATE DATABASE [{_dbName}]");
            await ExecuteDbCommandAsync(connection, $"USE [{_dbName}]");
        }

        public async Task DisposeAsync()
        {
            using var connection = new SqlConnection(BaseConnectionString);
            await connection.OpenAsync();

            await ExecuteDbCommandAsync(connection, $"EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'{_dbName}'");
            await ExecuteDbCommandAsync(connection, "USE [master]");
            await ExecuteDbCommandAsync(connection, $"ALTER DATABASE [{_dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            await ExecuteDbCommandAsync(connection, "USE [master]");
            await ExecuteDbCommandAsync(connection, $"DROP DATABASE [{_dbName}]");
        }

        private async Task ExecuteDbCommandAsync(SqlConnection connection, string commandText)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = commandText;
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
