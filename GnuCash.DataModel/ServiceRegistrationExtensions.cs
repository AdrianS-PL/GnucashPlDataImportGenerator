using GnuCash.DataModel.DatabaseModel;
using GnuCash.DataModel.Queries;
using GnuCash.DataModel.Queries.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GnuCash.DataModel
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddGnuCashDataModel(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GnuCashContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(nameof(GnuCashContext)),
                sqlServerOptions => sqlServerOptions.CommandTimeout(60)));

            services = AddQueries(services);

            return services;
        }

        private static IServiceCollection AddQueries(IServiceCollection services)
        {
            var queryTypes = Assembly.GetExecutingAssembly().GetTypes().Where(q => q.BaseType == typeof(GnuCashContextQueryBase));

            foreach(var queryType in queryTypes)
            {
                var queryInterface = queryType.GetTypeInfo().ImplementedInterfaces.Single(q => q.GetTypeInfo().ImplementedInterfaces.SingleOrDefault(r => r == typeof(IQuery)) != null);
                services.AddScoped(queryInterface, queryType);
            }

            return services;
        }
    }
}
