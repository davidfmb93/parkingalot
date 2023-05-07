using app.Models;
using app.Repositories;
using app.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("parking/[controller]")]
public class RegisterController : ControllerBase
{
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
        var regex = @"^(?!.*(ñ))[ABCDFGHJKLMNPRSTVWXYZ]{3}-[0-9]{5}";

        var match = new Regex(regex);

        if (!match.IsMatch(vehicle.NumberPlate))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{vehicle.NumberPlate} isn't right Format ABC-12345.");
        }
        var record = await _parkingRepository.AddVehicleAsync(vehicle);

        if (record == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{vehicle.NumberPlate} could not be added.");
        }

        return StatusCode(StatusCodes.Status200OK, record);
    }

    [HttpPost("/check-out/{NumberPlate}")]
    public async Task<ActionResult<Checkout>> CheckOut(String NumberPlate)
    {
        Vehicle vehicle = await _parkingRepository.GetVehicleAsync(NumberPlate);

        if (vehicle == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{NumberPlate} could not be updated.");
        }

        if (vehicle.Times.Count == 0 || (vehicle.Times.Last().EndTime != null & vehicle.MemberShip != MemberShip.Resident))
        {
            return StatusCode(StatusCodes.Status204NoContent, $"{NumberPlate} please do check-in.");
        }

        Checkout checkoutVehicle = await _parkingRepository.CheckoutVehicleAsync(vehicle);

        if (checkoutVehicle == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"{NumberPlate} could not be updated");
        }

        return StatusCode(StatusCodes.Status200OK, checkoutVehicle);
    }
}
