using Application.DriverBL;
using Application.DriverInfringementBL;
using Application.InfringementBL;
using Application.VehicleBL;
using AutoMapper;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Infrastructure.Middleware;

namespace WebApi
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
            // Add Connection String for SQL Server
            services.AddDbContext<dbInnovationContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Declaraciones de Dependencia Inyection MediatR
            services.AddMediatR(typeof(GetDriver.Handler).Assembly);
            services.AddMediatR(typeof(GetVehicle.Handler).Assembly);
            services.AddMediatR(typeof(GetInfringement.Handler).Assembly);
            services.AddMediatR(typeof(GetDriverInfringement.Handler).Assembly);
            services.AddControllersWithViews();

            // Dependency Inyection for Respositories
            services.AddScoped<IDriverRepository, DriverRepository>();
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
            services.AddScoped<IInfringementRepository, InfringementRepository>();
            services.AddScoped<IDriverVehicleRepository, DriverVehicleRepository>();
            services.AddScoped<IDriverInfringementRepository, DriverInfringementRepository>();

            // Add Swagger to the proyect, Documentation for API
            services.AddAutoMapper(typeof(GetDriverInfringementTop5.Handler));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Innovation Strategies API Services",
                    Version = "v1",
                    Description = "Prueba Técnica para Innovation Strategies",
                    Contact = new OpenApiContact()
                    {
                        Email = "armincancun@gmail.com",
                        Name = "Armin Cetina Mac"
                    }
                });
                c.CustomSchemaIds(c => c.FullName);
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Create a Middleware for Json Error in API
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            // Declare EndPoint For Api
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerPath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{ swaggerPath}/swagger/v1/swagger.json", "Shared Economy API Services V1");
            });

            // Declared API Controller
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            // Declare SPA VIEW
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
