using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.ExceptionService;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Logging;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.WeatherForecast_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Photo_Service;
using System.IdentityModel.Tokens.Jwt;

namespace Intel.EmployeeManagement.BlazorClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var employeeWebApiBaseAddress = builder.Configuration["EmployeeWebApiBaseAddress"];
            var identityProviderBaseAddress = builder.Configuration["IdentityProviderBaseAddress"];
            var photoServiceBaseAddress = builder.Configuration["PhotoServiceBaseAddress"];

            //AddHttpClient is the same as AddScoped.
            //Scoped services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddHttpClient<AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(identityProviderBaseAddress));
            builder.Services.AddScoped<IAuthenticationService>(provider => provider.GetRequiredService<AuthenticationService>());
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthenticationService>());
            builder.Services.AddHttpClient<IHttpService, HttpService>(httpClient => httpClient.BaseAddress = new Uri(employeeWebApiBaseAddress)); ; //Since HttpService uses AuthenticationService(scoped) which uses AuthenticationStateProviderService(scoped), and so HttpService cannot be singleton for a webassembly/client side Blazor as a singleton cannot consume scoped services.
            builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped<IDivideByZeroService, DivideByZeroService>();
            builder.Services.AddHttpClient<IPhotoService, PhotoService>(httpClient => httpClient.BaseAddress = new Uri(photoServiceBaseAddress));

            //Singleton services
            builder.Services.AddSingleton<IAppStateService, AppStateService>();            

            //Difference among Singleton, Scoped and Transient services
            builder.Services.AddSingleton<ISingletonTimeService, SingletonTimeService>();
            builder.Services.AddScoped<IScopedTimeService, ScopedTimeService>();
            builder.Services.AddTransient<ITransientTimeService, TransientTimeService>();

            builder.Services.AddLogging(loggingBuilder =>
            {
                var httpService = builder.Services.BuildServiceProvider().GetRequiredService<IHttpService>();
                loggingBuilder.SetMinimumLevel(LogLevel.Error);
                loggingBuilder.ClearProviders();
                loggingBuilder.AddProvider(new ApplicationLoggerProvider(httpService));
            });
          
            // You need to add the following two methods for Authorization to work in the web assembly blazor. In the server sice, you don't need them, they are already built in.
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();

            //https://stackoverflow.com/questions/66521906/error-when-attempting-to-decode-json-web-token-returned-from-own-backend-api
            //If we don't put the following code, we get the following error when published to a file system to run in IIS
            //IDX12729: Unable to decode the header 'System.String' as Base64Url encoded string. jwtEncodedString: 'System.String'.
            //But the above error does not come up with IIS Express.
             _ = new JwtHeader();
             _ = new JwtPayload();
        }
    }
}
