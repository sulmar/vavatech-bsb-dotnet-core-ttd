using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class ConnectionTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
    {
        private readonly DatabaseFixture _fixture;

        private readonly SqlConnection _connection;

        public ConnectionTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _connection = new SqlConnection(_fixture.ConnectionString);
        }

        public async Task InitializeAsync()
        {
            await _connection.OpenAsync();
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }

        [Fact]
        public async Task Can_Connect()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT 1";

            var result = await command.ExecuteScalarAsync();

            Assert.Equal(1, result);
        }
    }
}
