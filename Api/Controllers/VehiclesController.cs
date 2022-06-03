using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        // GET api/vehicles/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Debugger.Launch();

            Vehicle vehicle = vehicleRepository.Get(id);

            
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }

        // DELETE api/vehicles/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicle = vehicleRepository.Get(id);

            vehicleRepository.Remove(id);
            
            return RedirectToAction("Vehicles");
        }
    }
}
