using CarShowroom.Application.Interfaces;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Interfaces;
using CarShowroom.Domain.Models;
using CarShowroom.Domain.Models.DTO;
using CarShowroom.Domain.Models.Messaging;
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
            services.AddScoped<ICarRepository<CarDto>, CarRepository>();
            services.AddScoped<IMessageRepository<MessagePostDto, MessageGetDto>, MessageRepository>();
            services.AddScoped(typeof(MessageHub));

            // CarShowroom.Application
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IMessageService<MessagePostDto, MessageGetDto>, MessageService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
