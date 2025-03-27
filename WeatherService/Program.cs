using WeatherService.Services;
using WeatherService.Services.Interfaces;
using WeatherService.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<DateUtil>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IWeatherServiceForeCast, WeatherServiceForecast>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();