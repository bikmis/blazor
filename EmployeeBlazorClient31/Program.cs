using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RazorClassLibrary31.Services.EmployeeService;
using RazorClassLibrary31.Services.GuidService;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.AuthenticationService;
using RazorClassLibrary31.Services.SerializerService;
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
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => client.BaseAddress = new Uri("https://localhost:44382/")); 
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //Difference among AddSingleton, AddScoped and AddTransient
            builder.Services.AddSingleton<IGuidServiceAddSingleton, GuidService>();
            builder.Services.AddScoped<IGuidServiceAddScoped, GuidService>();
            builder.Services.AddTransient<IGuidServiceAddTransient, GuidService>();

            builder.Services.AddSingleton<IHttpService, HttpService>();
            builder.Services.AddSingleton<ISerializerService, SerializerService>();
            builder.Services.AddSingleton<ITokenService, TokenService>();

            builder.Services.AddScoped<AuthenticationStateProvider, RouteGuardService>();

            // You need to add the following two methods for Authorization to work in the web assembly blazor. In the server sice, you don't need them, they are already built in.
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
