using Intel.EmployeeManagement.RazorClassLibrary.Services.AppStore_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Guid_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Serializer_Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Intel.EmployeeManagement.BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var employeeBaseAddress = builder.Configuration["EmployeeBaseAddress"];
            var identityProviderBaseAddress = builder.Configuration["IdentityProviderBaseAddress"];

            //AddHttpClient() method will be availble after you install Microsoft.Extensions.Http
            //AddHttpClient is the same as AddScoped except that you can set its BaseAdress.
            //Scoped services
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(httpClient => httpClient.BaseAddress = new Uri(employeeBaseAddress));
            builder.Services.AddHttpClient<AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(identityProviderBaseAddress));
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthenticationService>());
            builder.Services.AddScoped<IHttpService, HttpService>(); //Since HttpService uses AuthenticationService(scoped) which uses AuthenticationStateProviderService(scoped), and so HttpService cannot be singleton for a webassembly/client side Blazor as a singleton cannot consume scoped services.

            //Singleton services
            builder.Services.AddSingleton<IAppStoreService, AppStoreService>();
            //The following HttpClient is created for Weather Forecast (/fetchdata which gets json data from weather.json file in the server). Check Network in the browser
            //to find request url https://localhost:44391/_content/RazorClassLibrary31/sample-data/weather.json, which is the blazor server.
            //Make a change to the weather.json in the server and revisit the /fetchdata page to see the new content. In the server side blazor,
            //the browser does not hold data in the cache but in the client side one clear browser cache, refresh the browser to view the changed content.
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<ISerializerService, SerializerService>();
            

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
