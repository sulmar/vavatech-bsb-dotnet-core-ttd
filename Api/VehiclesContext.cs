using Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class VehiclesContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
