using EmployeeBlazor.Services.EmployeeService;
using EmployeeBlazor.Services.UserService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //AddHttpClient() method will be availble after you install Microsoft.Extensions.Http
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
