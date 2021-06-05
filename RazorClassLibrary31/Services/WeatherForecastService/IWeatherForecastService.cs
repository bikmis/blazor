using Intel.EmployeeManagement.RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.RazorClassLibrary.Services.WeatherForecast_Service
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast[]> ListWeatherForecast();
    }
}
