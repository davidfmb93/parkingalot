using Microsoft.AspNetCore.Mvc;

namespace app.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastDavidController : ControllerBase
{
    //private static readonly string[] Summaries = new[]
    //{
    //    "Freezing1", "Bracing2", "Chilly34", "Cool4", "Mild543", "Warm31", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //private readonly ILogger<WeatherForecastDavidController> _logger;

    //public WeatherForecastDavidController(ILogger<WeatherForecastDavidController> logger)
    //{
    //    _logger = logger;
    //}

    //[HttpGet(Name = "GetWeatherForecastDavid")]
    //public IEnumerable<WeatherForecastDavid> Get()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecastDavid
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}

    //[HttpPost(Name = "GetWeatherForecastAngie4")]
    //public IEnumerable<WeatherForecastAngie> Post()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecastAngie
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}

    //[HttpPut(Name = "GetWeatherForecastBrianna")]
    //public IEnumerable<WeatherForecastAngie> Put()
    //{
    //    return Enumerable.Range(1, 5).Select(index => new WeatherForecastAngie
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
}
