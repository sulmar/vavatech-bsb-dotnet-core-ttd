using Ductus.FluentDocker.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class SqlServerDatabaseFixture : IAsyncLifetime
    {
        public SqlConnection Connection { get; }

        private IContainerService container;

        public SqlServerDatabaseFixture()
        {
                  
        }

        public void Dispose()
        {
            Connection.Dispose();
            container.Dispose();

        }

        public Task InitializeAsync()
        {
            var container = new Ductus.FluentDocker.Builders.Builder()
               .UseContainer()
               .UseCompose()
               .FromFile("./docker-compose.yml")
               .RemoveOrphans()
               .Build()
               .Start();

            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
