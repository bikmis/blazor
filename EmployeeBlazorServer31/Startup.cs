using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorClassLibrary31.Services.EmployeeService;
using RazorClassLibrary31.Services.GuidService;
using RazorClassLibrary31.Services.HttpService;
using RazorClassLibrary31.Services.LoginService;
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

            services.AddScoped(s =>
            {
                var navigationManager = s.GetRequiredService<NavigationManager>();
                return new HttpClient { BaseAddress = new Uri(navigationManager.BaseUri) };
            });

            services.AddHttpClient<IEmployeeService, EmployeeService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            services.AddHttpClient<ILoginService, LoginService>(client => client.BaseAddress = new Uri("https://localhost:44327/"));
            services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/"));
            //Difference among AddSingleton, AddScoped and AddTransient
            services.AddSingleton<IGuidServiceAddSingleton, GuidService>();
            services.AddScoped<IGuidServiceAddScoped, GuidService>();
            services.AddTransient<IGuidServiceAddTransient, GuidService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddSingleton<ISerializerService, SerializerService>();
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
