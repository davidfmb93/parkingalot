using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("/api/vehicle/[controller]")]
[DisplayName("Your New Tag")]
public class ReportController  : ControllerBase
{
    private readonly ILogger<ReportController > _logger;

    public ReportController (ILogger<ReportController > logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public String Report()
    {
        return " Report... ";
    }
}
