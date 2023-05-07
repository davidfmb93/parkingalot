using app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("/api/vehicle/[controller]")]
public class RestartController : ControllerBase
{
    private readonly ILogger<RestartController> _logger;
    private readonly IParkingRepository _parkingRepository;

    public RestartController(ILogger<RestartController> logger, IParkingRepository parkingRepository)
    {
        _logger = logger;
        _parkingRepository = parkingRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Restart()
    {
        (bool status, string message) = await _parkingRepository.DeleteTimesAsync();

        if (status == false)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }

        return StatusCode(StatusCodes.Status200OK, message);
    }
}
