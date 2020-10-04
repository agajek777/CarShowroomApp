using Autofac;
using CarShowroom.Application.Services;
using CarShowroom.Domain.Models.Messaging;
using CarShowroom.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarShowroom.Infra.IoC
{
    public static class ContainerConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            // CarShowroom.Domain
            builder.RegisterType<CarRepository>().AsImplementedInterfaces();
            builder.RegisterType<MessageRepository>().AsImplementedInterfaces();
            builder.RegisterType<MessageHub>().AsImplementedInterfaces();

            // CarShowroom.Domain
            builder.RegisterType<CarService>().AsImplementedInterfaces();
            builder.RegisterType<MessageService>().AsImplementedInterfaces();
            builder.RegisterType<JwtService>().AsImplementedInterfaces();
        }
    }
}
