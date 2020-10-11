using GnuCash.CommodityPriceImportGenerator.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddCommodityPriceImportGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CommodityPriceImportGeneratorSettings>(configuration.GetSection(nameof(CommodityPriceImportGeneratorSettings)));
            services.AddTransient<IPriceImportGenerator, PriceImportGenerator>();
            //services.AddHttpClient<BossaWebsiteClient>();

            return services;
        }
    }
}
