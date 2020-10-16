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
            var assemblyDomain = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Domain)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Domain)}.dll");
            builder.RegisterAssemblyTypes(assemblyDomain).Where(t => t.Namespace.EndsWith(nameof(Domain.Models.Messaging))).AsImplementedInterfaces();

            var assemblyInfraData = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Infra)}.{nameof(Data)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Infra)}.{nameof(Data)}.dll");
            builder.RegisterAssemblyTypes(assemblyInfraData).Where(t => t.Namespace.EndsWith(nameof(Data.Repositories))).AsImplementedInterfaces();

            var assemblyApplication = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Application)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Application)}.dll");
            builder.RegisterAssemblyTypes(assemblyApplication).Where(t => t.Namespace.EndsWith(nameof(Application.Services))).AsImplementedInterfaces();
        }
    }
}
