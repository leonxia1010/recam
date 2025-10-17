using Microsoft.AspNetCore.Mvc;

namespace Remp.API.Controllers;

/// <summary>
/// Controller for managing weather forecast operations
/// </summary>
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    /// <summary>
    /// Initializes a new instance of the WeatherForecastController
    /// </summary>
    /// <param name="logger">Logger instance for tracking controller operations</param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Gets a 5-day weather forecast with randomly generated temperature data
    /// </summary>
    /// <returns>Returns an array of weather forecast objects</returns>
    /// <response code="200">Successfully returns weather forecast data</response>
    /// <response code="404">Weather forecast data not found</response>
    [HttpGet(Name = "GetWeatherForecast")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
