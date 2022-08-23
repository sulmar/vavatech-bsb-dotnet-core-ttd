using Api.Infrastructure;
using Api.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class VehicleRepositoryFactory
    {
        private readonly IServiceProvider serviceProvider;

        public VehicleRepositoryFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IVehicleRepository GetVehicleRepository(string userSelection)
        {
            switch(userSelection)
            {
                case "Oracle":
                    return (IVehicleRepository)serviceProvider.GetService(typeof(OracleDbVehicleRepository));
                case "SqlServer":
                    return (IVehicleRepository)serviceProvider.GetService(typeof(SqlServerDbVehicleRepository));

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
