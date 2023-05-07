using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace app.Controllers.Vehicles;

[ApiController]
[Route("/api/vehicle/[controller]")]
[DisplayName("Your New Tag")]
public class RestartController  : ControllerBase
{
    private readonly ILogger<RestartController > _logger;

    public RestartController (ILogger<RestartController > logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public String Restart()
    {
        return " Restarting... ";
    }
}
