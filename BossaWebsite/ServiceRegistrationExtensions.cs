using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BossaWebsite
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddBossaWebsiteClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BossaWebsiteSettings>(configuration.GetSection(nameof(BossaWebsiteSettings)));
            services.AddHttpClient<BossaWebsiteClient>();
            return services;
        }
    }
}
