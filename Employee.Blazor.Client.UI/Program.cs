using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Razor.Components.Library.Services.EmployeeService;
using Razor.Components.Library.Services.UserService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Employee.Blazor.Client.UI
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
