using Autofac;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Identity;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.Infra.Data.Context;
using CarShowroom.Infra.IoC;
using CarShowroom.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CarShowroomApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServicesConfiguration.Configure(services, Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            ContainerConfig.Configure(builder);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var isTesting = string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"), "Testing", StringComparison.InvariantCultureIgnoreCase);

            if (isTesting)
            {
                var context = app.ApplicationServices.GetService<DatabaseContext<User, Role>>();

                AddTestData(context);
            }

            if (env.IsDevelopment() || isTesting)
            {
                app.UseDeveloperExceptionPage();

                loggerFactory.AddLog4Net();
            }
            else
                loggerFactory.AddLog4Net("/app/Logging/log4net.config");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/chat");
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Carshowroom WEB API"));
        }

        public void AddTestData(DatabaseContext<User, Role> context)
        {
            if (context.Cars.Any())
                return;

            context.Cars.AddRange(
            new Car()
                {
                    Brand = "Test",
                    Description = "Test",
                    Engine = "Test",
                    ImagePath = "Test",
                    Mileage = 100.0,
                    Model = "Test",
                    Power = 100,
                    Price = 100.0,
                    Production = DateTime.Now
                },
                    new Car()
                {
                    Brand = "Test2",
                    Description = "Test2",
                    Engine = "Test2",
                    ImagePath = "Test2",
                    Mileage = 100.0,
                    Model = "Test2",
                    Power = 100,
                    Price = 100.0,
                    Production = DateTime.Now
                }
            );

            context.SaveChanges();
        }
    }
}
