using Intel.EmployeeManagement.Data;
using Intel.EmployeeManagement.IdentityProvider.Services.Database_Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Intel.EmployeeManagement.IdentityProvider
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IDatabaseService, DatabaseService>();

            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            ));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = "https://localhost:44382/",  //token server base address
                    ValidateIssuer = true,
                    ValidIssuer = "https://localhost:44382/", //token server base address
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["RefreshTokenSecurityKey"])),
                    ClockSkew = TimeSpan.Zero  // default is 5 minutes, i.e. the token would be valid up to 5 minutes after expiry. With TimeSpan.Zero, the token will be invalid exactly at expiration.
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // you need to add this for jwt to work (services.AddAuthentication(...) in ConfigureServices in this startup file.

            app.UseRouting();

            app.UseCors(); //cors need to be after routing and before authorization

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
