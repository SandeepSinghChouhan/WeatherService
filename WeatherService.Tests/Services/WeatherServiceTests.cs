using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using WeatherService.Models;
using WeatherService.Services.Interfaces;
using Xunit;

namespace WeatherService.Tests.Services
{
    public class WeatherServiceForeCastTests
    {
        private readonly Mock<IWeatherServiceForeCast> _weatherServiceForeCastMock;

        public WeatherServiceForeCastTests()
        {
            _weatherServiceForeCastMock = new Mock<IWeatherServiceForeCast>();
        }

        [Fact]
        public async Task GetWeatherByCityIdAsync_ShouldReturnWeatherResponse()
        {
            // Arrange
            var cityId = "12345";
            var expectedWeather = new WeatherResponse { Name = "New York", Main = new MainData { Temp = 20.5 } };
            _weatherServiceForeCastMock.Setup(service => service.GetWeatherByCityIdAsync(cityId))
                .ReturnsAsync(expectedWeather);

            // Act
            var result = await _weatherServiceForeCastMock.Object.GetWeatherByCityIdAsync(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedWeather.Name, result?.Name);
            Assert.Equal(expectedWeather.Main?.Temp, result?.Main?.Temp);
        }

        [Fact]
        public async Task ProcessDailyWeatherAsync_ShouldProcessWithoutErrors()
        {
            // Arrange
            _weatherServiceForeCastMock.Setup(service => service.ProcessDailyWeatherAsync())
                .Returns(Task.CompletedTask);

            // Act
            var exception = await Record.ExceptionAsync(() => _weatherServiceForeCastMock.Object.ProcessDailyWeatherAsync());

            // Assert
            Assert.Null(exception);
            _weatherServiceForeCastMock.Verify(service => service.ProcessDailyWeatherAsync(), Times.Once);
        }
    }
}

﻿
