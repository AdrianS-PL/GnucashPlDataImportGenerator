using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StooqWebsite.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace StooqWebsite
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddStooqWebsiteClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StooqWebsiteSettings>(configuration.GetSection(nameof(StooqWebsiteSettings)) );
            services.AddHttpClient<StooqWebsiteClient>();
            return services;
        }
    }
}
