using WeatherService.Models;

namespace WeatherService.Services.Interfaces;

public interface IWeatherServiceForeCast
{
    Task<WeatherResponse?> GetWeatherByCityIdAsync(string cityId);
    Task ProcessDailyWeatherAsync();
}