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
            //AddHttpClient is the same as AddScoped
            //Scoped services
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => client.BaseAddress = new Uri("https://localhost:44382/"));
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationStateProviderService>();
            // With the following, everything works except that Authorization fails for unknown reason, and so HttpClient is not injected from here, rather HttpClient is created inside AuthenticationStateProviderService.
            // builder.Services.AddHttpClient<AuthenticationStateProvider, AuthenticationStateProviderService>(client => client.BaseAddress = new Uri("https://localhost:44382/"));

            //Singleton services
            builder.Services.AddSingleton<IUserService, UserService>();
            //The following HttpClient is created for Weather Forecast (/fetchdata which gets json data from weather.json file in the server). Check Network in the browser
            //to find request url https://localhost:44391/_content/RazorClassLibrary31/sample-data/weather.json, which is the blazor server.
            //Make a change to the weather.json in the server and revisit the /fetchdata page to see the new content. In the server side blazor,
            //the browser does not hold data in the cache but in the client side one clear browser cache, refresh the browser to view the changed content.
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IHttpService, HttpService>();
            builder.Services.AddSingleton<ISerializerService, SerializerService>();
            builder.Services.AddSingleton<ITokenService, TokenService>();

            //Difference among AddSingleton, AddScoped and AddTransient
            builder.Services.AddSingleton<IGuidServiceAddSingleton, GuidService>();
            builder.Services.AddScoped<IGuidServiceAddScoped, GuidService>();
            builder.Services.AddTransient<IGuidServiceAddTransient, GuidService>();


            // You need to add the following two methods for Authorization to work in the web assembly blazor. In the server sice, you don't need them, they are already built in.
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
