using app.Repositories;
using Microsoft.EntityFrameworkCore;
using app.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("/api/vehicle/[controller]")]
[DisplayName("Your New Tag")]
public class RolController  : ControllerBase
{
    private readonly ILogger<RolController > _logger;

    private readonly IParkingRepository _parkingRepository;

    public RolController (ILogger<RolController > logger, IParkingRepository parkingRepository)
    {
        _logger = logger;
        _parkingRepository = parkingRepository;
    }

    [HttpPut("{Rol}/{NumberPlate}")]
    public async Task<IActionResult> Put(MemberShip Rol, String NumberPlate)
    {
            Vehicle vehicle = await _parkingRepository.GetVehicleAsync(NumberPlate);

            if (vehicle == null)
            {
                return BadRequest();
            }

            vehicle.MemberShip = Rol;
            Vehicle updateVehicle = await _parkingRepository.UpdateVehicleAsync(vehicle);


            if (updateVehicle == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{NumberPlate} could not be updated");
            }

            return NoContent();
    }
}
