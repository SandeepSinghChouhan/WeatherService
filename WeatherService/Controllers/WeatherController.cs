using Microsoft.AspNetCore.Mvc;
using WeatherService.Services.Interfaces;

namespace WeatherService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherServiceForeCast _weatherService;

    public WeatherController(IWeatherServiceForeCast weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("process")]
    public async Task<IActionResult> ProcessWeather()
    {
        await _weatherService.ProcessDailyWeatherAsync();
        return Ok("Weather data processed successfully");
    }
}