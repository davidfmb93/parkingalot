using app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("/api/vehicle/[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly IParkingRepository _parkingRepository;

    public ReportController(ILogger<ReportController> logger, IParkingRepository parkingRepository)
    {
        _logger = logger;
        _parkingRepository = parkingRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetReport()
    {
        var report = await _parkingRepository.GetPaymentResidentsAsync();

        if (report == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No report in database");
        }

        return StatusCode(StatusCodes.Status200OK, report);
    }
}
