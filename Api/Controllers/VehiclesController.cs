using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly ILogger<VehiclesController> logger;

        public VehiclesController(IVehicleRepository vehicleRepository, ILogger<VehiclesController> logger)
        {
            this.vehicleRepository = vehicleRepository;
            this.logger = logger;
        }

        // GET api/vehicles/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Vehicle vehicle = vehicleRepository.Get(id);

            if (vehicle == null)
                return NotFound();

            logger.LogInformation("Get vehicle id={id}", id);

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
