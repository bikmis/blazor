using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary31.Services.EmployeeService;
using RazorClassLibrary31.Services.GuidService;
using RazorClassLibrary31.Services.LoginService;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeBlazorClient31
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //AddHttpClient() method will be availble after you install Microsoft.Extensions.Http
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            builder.Services.AddHttpClient<ILoginService, LoginService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //Difference among AddSingleton, AddScoped and AddTransient
            builder.Services.AddSingleton<IGuidServiceAddSingleton, GuidService>();
            builder.Services.AddScoped<IGuidServiceAddScoped, GuidService>();
            builder.Services.AddTransient<IGuidServiceAddTransient, GuidService>();

            builder.Services.AddScoped<ITokenService, TokenService>();

            await builder.Build().RunAsync();
        }
    }
}
