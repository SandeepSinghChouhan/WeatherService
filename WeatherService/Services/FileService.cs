using System.Text.Json;
using WeatherService.Models;
using WeatherService.Services.Interfaces;

namespace WeatherService.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Dictionary<string, string> ReadCitiesFile()
    {
        var cities = new Dictionary<string, string>();
        var citiesFilePath = _configuration["CitiesFilePath"] ?? "cities.txt";

        if (!File.Exists(citiesFilePath))
            throw new FileNotFoundException($"Cities file not found at: {citiesFilePath}");

        foreach (var line in File.ReadAllLines(citiesFilePath))
        {
            var parts = line.Split('=');
            if (parts.Length == 2)
            {
                cities[parts[0].Trim()] = parts[1].Trim();
            }
        }

        return cities;
    }

    public void SaveWeatherData(List<WeatherResponse?> weatherData, string dateStr)
    {
        var outputFolderPath = _configuration["OutputFolderPath"] ?? "output";

        if (!Directory.Exists(outputFolderPath))
            Directory.CreateDirectory(outputFolderPath);

        var options = new JsonSerializerOptions { WriteIndented = true };

        foreach (var data in weatherData)
        {
            if (data?.Name == null) continue;

            var fileName = Path.Combine(
                outputFolderPath,
                $"{data.Name}_{dateStr}.json");

            File.WriteAllText(
                fileName,
                JsonSerializer.Serialize(data, options));
        }
    }
}