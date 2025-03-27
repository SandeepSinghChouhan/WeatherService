using WeatherService.Models;

namespace WeatherService.Services.Interfaces;

public interface IFileService
{
    Dictionary<string, string> ReadCitiesFile();
    void SaveWeatherData(List<WeatherResponse?> weatherData, string dateStr);
}