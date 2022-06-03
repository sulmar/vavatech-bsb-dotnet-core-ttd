using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.IRepositories
{
    public interface IEntityRepository<TEntity>
    {
        TEntity Get(int id);
        void Remove(int id);
    }

    public interface IVehicleRepository : IEntityRepository<Vehicle>
    {
     
    }

    public interface IProductRepository : IEntityRepository<Product>
    {
       

    }
}
