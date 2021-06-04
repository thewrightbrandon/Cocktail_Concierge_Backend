using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DrinksAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DrinksAPI
{
    public class Startup
    {
        // Constructor: Grabbing the configurtation object that will let us grab variables from appsettings.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // the property to receive the configuration object
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // The collects all our controllers for routing
            services.AddControllers();

            // We save the connection string appsettings.json in a variable
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            // We register the db context as a service
            services.AddDbContext<DrinkContext>(opt => opt.UseNpgsql(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrinksAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrinksAPI v1"));
            }

            // forces site to redirect to https, comment out for now
            // app.UseHttpsRedirection();

            app.UseRouting(); // enables writing

            app.UseAuthorization(); // enables authorization

            // this function is where we define all our routing
            app.UseEndpoints(endpoints =>
            {
                    // Enable Attribute Routing
                    endpoints.MapControllers();
                    // Enable Pattern Matching
                    endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{URLParam?}");
            });
        }
    }
}
