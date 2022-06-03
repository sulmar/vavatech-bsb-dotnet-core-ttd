using Api.IRepositories;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Infrastructure
{
    public class SqlServerDbVehicleRepository : IVehicleRepository
    {
        public Vehicle Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class OracleDbVehicleRepository : IVehicleRepository
    {
        public Vehicle Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class DbVehicleRepository : IVehicleRepository
    {
        private readonly VehiclesContext context;

        public DbVehicleRepository(VehiclesContext context)
        {
            this.context = context;
        }

        public Vehicle Get(int id)
        {
            return context.Vehicles.Find(id);            
        }

        public void Remove(int id)
        {
            var vehicle = Get(id);
            context.Vehicles.Remove(vehicle);
            context.SaveChanges();
        }
    }
}
