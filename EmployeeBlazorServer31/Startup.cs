using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorClassLibrary31.Services.EmployeeService;
using RazorClassLibrary31.Services.GuidService;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.Auth;
using RazorClassLibrary31.Services.SerializerService;
using RazorClassLibrary31.Services.TokenService;
using RazorClassLibrary31.Services.UserService;
using System;
using System.Net.Http;

namespace EmployeeBlazorServer31
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

            //Scoped services
            //The following HttpClient is created for Weather Forecast (/fetchdata which gets json data from weather.json file in the server). Check Network in the browser
            //to find request url https://localhost:44391/_content/RazorClassLibrary31/sample-data/weather.json, which is the blazor server.
            //Make a change to the weather.json in the server and revisit the /fetchdata page to see the new content. In the server side blazor,
            //the browser does not hold data in the cache but in the client side one clear browser cache, refresh the browser to view the changed content.
            services.AddScoped(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
            });
            services.AddHttpClient<IEmployeeService, EmployeeService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
            services.AddScoped<IHttpService, HttpService>(); //For Blazor server, HttpService needs to be scoped and cannot be a singleton as a singleton cannot consume IJSRuntime which is scoped in Blazor Server (but IJSRuntime is singleton for WebAssembly/client side Blazor)

            //Singleton services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ISerializerService, SerializerService>();
            services.AddSingleton<ITokenService, TokenService>();            

            //Difference among AddSingleton, AddScoped and AddTransient
            services.AddSingleton<IGuidServiceAddSingleton, GuidService>();
            services.AddScoped<IGuidServiceAddScoped, GuidService>();
            services.AddTransient<IGuidServiceAddTransient, GuidService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
