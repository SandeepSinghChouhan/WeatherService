using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using WeatherService.Models;
using WeatherService.Services.Interfaces;
using Xunit;

namespace WeatherService.Tests.Services
{
    public class FileServiceTests
    {
        private readonly Mock<IFileService> _fileServiceMock;

        public FileServiceTests()
        {
            _fileServiceMock = new Mock<IFileService>();
        }

        [Fact]
        public void ReadCitiesFile_ShouldReturnDictionary()
        {
            // Arrange
            var expectedCities = new Dictionary<string, string>
            {
                { "New York", "NY" },
                { "Los Angeles", "CA" }
            };
            _fileServiceMock.Setup(service => service.ReadCitiesFile()).Returns(expectedCities);

            // Act
            var result = _fileServiceMock.Object.ReadCitiesFile();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCities.Count, result.Count);
            Assert.Equal(expectedCities["New York"], result["New York"]);
        }

        [Fact]
        public void SaveWeatherData_ShouldSaveData()
        {
            // Arrange
            var weatherData = new List<WeatherResponse?>
            {
                new WeatherResponse { Name = "New York", Main = new MainData { Temp = 20.5 } },
                new WeatherResponse { Name = "Los Angeles", Main = new MainData { Temp = 25.3 } }
            };
            var dateStr = "2023-10-01";

            // Act
            _fileServiceMock.Object.SaveWeatherData(weatherData, dateStr);

            // Assert
            _fileServiceMock.Verify(service => service.SaveWeatherData(weatherData, dateStr), Times.Once);
        }
    }
}
