using Intel.EmployeeManagement.RazorClassLibrary.Services.AppState_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Authentication_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.ExceptionService;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Employee_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Http_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Logging;
using Intel.EmployeeManagement.RazorClassLibrary.Services.Time_Service;
using Intel.EmployeeManagement.RazorClassLibrary.Services.WeatherForecast_Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Intel.EmployeeManagement.BlazorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var resourceBaseAddress = Configuration["ResourceBaseAddress"];
            var identityProviderBaseAddress = Configuration["IdentityProviderBaseAddress"];
            var frontendBaseAddress = Configuration["FrontendBaseAddress"];

            /*
            services.AddScoped(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
            });
            */

            //Scoped services
            services.AddScoped<IEmployeeService, EmployeeService>();
          
            
            services.AddHttpClient<AuthenticationService>(httpClient => httpClient.BaseAddress = new Uri(identityProviderBaseAddress)); ;
            services.AddScoped<IAuthenticationService>(provider => provider.GetRequiredService<AuthenticationService>());
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthenticationService>());

            services.AddHttpClient<IHttpService, HttpService>(httpClient => httpClient.BaseAddress = new Uri(resourceBaseAddress)); //For Blazor server, HttpService needs to be scoped and cannot be a singleton as a singleton cannot consume IJSRuntime which is scoped in Blazor Server (but IJSRuntime is singleton for WebAssembly/client side Blazor)
            services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(httpClient => httpClient.BaseAddress = new Uri(frontendBaseAddress));
            services.AddScoped<IDivideByZeroService, DivideByZeroService>();

            //Singleton services
            services.AddSingleton<IAppStateService, AppStateService>();

            //Difference among Singleton, Scoped and Transient services
            services.AddSingleton<ISingletonTimeService, SingletonTimeService>();
            services.AddScoped<IScopedTimeService, ScopedTimeService>();
            services.AddTransient<ITransientTimeService, TransientTimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {         
            var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
            var httpService = serviceProvider.GetRequiredService<IHttpService>();
            loggerFactory.AddProvider(new ApplicationLoggerProvider(httpService));
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
