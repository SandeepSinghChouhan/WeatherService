using System.Text.Json;
using WeatherService.Models;
using WeatherService.Services.Interfaces;
using WeatherService.Utilities;

namespace WeatherService.Services;

public class WeatherServiceForecast : IWeatherServiceForeCast
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFileService _fileService;
    private readonly DateUtil _dateUtil;
    private readonly IConfiguration _configuration;

    public WeatherServiceForecast(
        IHttpClientFactory httpClientFactory,
        IFileService fileService,
        DateUtil dateUtil,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _fileService = fileService;
        _dateUtil = dateUtil;
        _configuration = configuration;
    }

    public async Task<WeatherResponse?> GetWeatherByCityIdAsync(string cityId)
    {
        var client = _httpClientFactory.CreateClient();
        var apiUrl = _configuration["OpenWeather:ApiUrl"];
        var apiKey = _configuration["OpenWeather:ApiKey"];

        var url = $"{apiUrl}?id={cityId}&appid={apiKey}&units=metric";
        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<WeatherResponse>(content);
    }

    public async Task ProcessDailyWeatherAsync()
    {
        var cities = _fileService.ReadCitiesFile();
        var weatherData = new List<WeatherResponse?>();

        foreach (var city in cities)
        {
            var weather = await GetWeatherByCityIdAsync(city.Key);
            if (weather != null)
            {
                weatherData.Add(weather);
            }
        }

        var dateStr = _dateUtil.GetCurrentDateFormatted();
        _fileService.SaveWeatherData(weatherData, dateStr);
    }
}