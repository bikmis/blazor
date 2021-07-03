using Intel.EmployeeManagement.RazorClassLibrary.Models;
using Xunit;

namespace Intel.EmployeeManagement.Tests.Blazor_Client_Tests.Unit_Tests
{
    public class WeatherForecastTests
    {
        [Fact(DisplayName = "Zero Celsius equals 32 Fahrenheit")]
        public void TestCelsiusToFahrenheitConversion()
        {
            var forecast = new WeatherForecast();
            forecast.TemperatureC = 0;
            Assert.Equal(32, forecast.TemperatureF);
        }

        [Theory(DisplayName = "Below 0 Celsius will be below 32 Fahrenheit")]
        [InlineData(-3)]
        [InlineData(-8)]
        [InlineData(-50)]
        public void TestBelowZeroCelsius(int value)
        {
            var weatherForecast = new WeatherForecast();
            weatherForecast.TemperatureC = value;
            Assert.True(weatherForecast.TemperatureF < 32);
        }
    }
}
