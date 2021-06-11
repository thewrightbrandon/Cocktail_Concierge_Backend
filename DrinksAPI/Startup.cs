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
        // Constructor -> grabbing the config object
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // the property to receive the config object
        public IConfiguration Configuration { get; }

        // method is used to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Collects all our controllers for routing
            services.AddControllers();

            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            // We register the db context as a service
            services.AddDbContext<DrinkContext>(opt => opt.UseNpgsql(connectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrinksAPI", Version = "v1" });
            });
        }

        // method is used to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DrinksAPI v1"));
            }

            // app.UseHttpsRedirection()

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("MyPolicy"); // enables CORS

            // define the routing
            app.UseEndpoints(endpoints =>
            {
                // Enable Attribute Routing
                endpoints.MapControllers().RequireCors("MyPolicy");
                // Enable Pattern Matching
                endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{URLParam?}");
            });
        }
    }
}
