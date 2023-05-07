using Microsoft.AspNetCore.Mvc;
using app.Repositories;
using app.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("parking/[controller]")]
public class RegisterController : ControllerBase
{
    private Double PRICE = 0.05;
        
    private readonly ILogger<RegisterController> _logger;

    private readonly IParkingRepository _parkingRepository;

    public RegisterController(ILogger<RegisterController> logger, IParkingRepository parkingRepository)
    {
        _logger = logger;
        _parkingRepository = parkingRepository;
    }

    [HttpPost("/check-in")]
    public async Task<ActionResult<Vehicle>> CheckIn(Vehicle vehicle)
    {
        var record = await _parkingRepository.AddVehicleAsync(vehicle);

        if (record == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{vehicle.NumberPlate} could not be added.");
        }

        return StatusCode(StatusCodes.Status200OK, record);
    }

    [HttpPost("/check-out/{NumberPlate}")]
    public async Task<ActionResult<Vehicle>> CheckOut(String NumberPlate)
    {
             Vehicle vehicle = await _parkingRepository.GetVehicleAsync(NumberPlate);

            if (vehicle == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{NumberPlate} could not be updated.");
            }

            Vehicle checkoutVehicle = await _parkingRepository.CheckoutVehicleAsync(vehicle);

            if (checkoutVehicle == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{NumberPlate} could not be updated");
            }

            return StatusCode(StatusCodes.Status200OK, checkoutVehicle);
    }
}
