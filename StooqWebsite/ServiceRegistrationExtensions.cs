using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StooqWebsite
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddStooqWebsiteClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<StooqWebsiteClient>();
            return services;
        }
    }
}
