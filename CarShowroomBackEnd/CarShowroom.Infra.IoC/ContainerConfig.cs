using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarShowroom.Infra.IoC
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(CarShowroom.Application)))
                    .Where(t => t.Namespace.Contains(nameof(CarShowroom.Application.Services)))
                    .As(t => t.GetInterfaces()
                    .Where(t => t.Namespace.Contains(nameof(CarShowroom.Application.Interfaces)))
                    .FirstOrDefault(i => t.Name == "I" + t.Name));

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(CarShowroom.Infra.Data)))
                .Where(t => t.Namespace.Contains(nameof(CarShowroom.Infra.Data.Repositories)))
                    .As(t => t.GetInterfaces()
                    .Where(t => t.Namespace.Contains(nameof(CarShowroom.Domain.Interfaces)))
                    .FirstOrDefault(i => t.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
