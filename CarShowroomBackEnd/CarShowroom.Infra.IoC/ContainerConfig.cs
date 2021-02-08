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
            var isDevelopmentOrTesting = (
                string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "development", StringComparison.InvariantCultureIgnoreCase)
                ||
                string.Equals(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                "Testing", StringComparison.InvariantCultureIgnoreCase)
            );

            Assembly assemblyDomain, assemblyInfraData, assemblyApplication;

            if (isDevelopmentOrTesting)
            {
                assemblyDomain = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Domain)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Domain)}.dll");
                builder.RegisterAssemblyTypes(assemblyDomain).Where(t => t.Namespace.EndsWith(nameof(Domain.Models.Messaging))).AsImplementedInterfaces();

                assemblyInfraData = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Infra)}.{nameof(Data)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Infra)}.{nameof(Data)}.dll");
                builder.RegisterAssemblyTypes(assemblyInfraData).Where(t => t.Namespace.EndsWith(nameof(Data.Repositories))).AsImplementedInterfaces();

                assemblyApplication = Assembly.LoadFrom($"..\\{nameof(CarShowroom)}.{nameof(Application)}\\bin\\Debug\\netcoreapp3.1\\{nameof(CarShowroom)}.{nameof(Application)}.dll");
                builder.RegisterAssemblyTypes(assemblyApplication).Where(t => t.Namespace.EndsWith(nameof(Application.Services))).AsImplementedInterfaces();

                return;
            }

            assemblyDomain = Assembly.LoadFrom($"/app/{nameof(CarShowroom)}.{nameof(Domain)}.dll");
            builder.RegisterAssemblyTypes(assemblyDomain).Where(t => t.Namespace.EndsWith(nameof(Domain.Models.Messaging))).AsImplementedInterfaces();

            assemblyInfraData = Assembly.LoadFrom($"/app/{nameof(CarShowroom)}.{nameof(Infra)}.{nameof(Data)}.dll");
            builder.RegisterAssemblyTypes(assemblyInfraData).Where(t => t.Namespace.EndsWith(nameof(Data.Repositories))).AsImplementedInterfaces();

            assemblyApplication = Assembly.LoadFrom($"/app/{nameof(CarShowroom)}.{nameof(Application)}.dll");
            builder.RegisterAssemblyTypes(assemblyApplication).Where(t => t.Namespace.EndsWith(nameof(Application.Services))).AsImplementedInterfaces();
        }
    }
}
