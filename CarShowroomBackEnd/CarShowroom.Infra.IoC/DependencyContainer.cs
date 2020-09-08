using CarShowroom.Application.Interfaces;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShowroom.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // CarShowrrom.Domain
            services.AddScoped<ICarRepository<Car, CarDto>, CarRepository>();

            // CarShowroom.Application
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
