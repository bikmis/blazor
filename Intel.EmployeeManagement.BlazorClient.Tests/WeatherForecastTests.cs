using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using Xunit;

namespace Intel.EmployeeManagement.BlazorClient.Tests
{
    public class WeatherForecastTests
    {
        [Fact]
        public void WeatherForecastZeroCShouldBe32F()
        {
            var forecast = new WeatherForecast();
            forecast.TemperatureC = 0;
            Assert.Equal(32, forecast.TemperatureF);
        }

        [Theory]
        [InlineData(-3)]
        [InlineData(-8)]
        [InlineData(-50)]
        public void WeatherForecastBelowZeroC_ShouldBeBelow32F(int value) {
            var weatherForecast = new WeatherForecast();
            weatherForecast.TemperatureC = value;
            Assert.True(weatherForecast.TemperatureF < 32);        
        }
    }
}
