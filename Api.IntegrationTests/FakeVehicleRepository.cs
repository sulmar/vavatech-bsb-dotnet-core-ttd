using Api.IRepositories;
using Api.Models;
using System;

namespace Api.IntegrationTests
{
    // dotnet add package Microsoft.AspNetCore.TestHost

    public class FakeVehicleRepository : IVehicleRepository
    {
        public Vehicle Get(int id)
        {
            if (id == 0)
                return null;
            else
                return new Vehicle { Id = id };
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
