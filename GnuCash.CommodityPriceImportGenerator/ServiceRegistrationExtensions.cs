using GnuCash.CommodityPriceImportGenerator.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GnuCash.CommodityPriceImportGenerator.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace GnuCash.CommodityPriceImportGenerator
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddCommodityPriceImportGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CommodityPriceImportGeneratorSettings>(configuration.GetSection(nameof(CommodityPriceImportGeneratorSettings)));
            services.Configure<PolishTreasuryBondsPriceImportGeneratorSettings>(configuration.GetSection(nameof(PolishTreasuryBondsPriceImportGeneratorSettings)));
            services.AddTransient<IPriceImportGenerator, PriceImportGenerator>();
            services.AddPolishTreasuryBondsAccountStateFileParsers();
            services.AddTransient<IPolishTreasuryBondsPriceImportGenerator, PolishTreasuryBondsPriceImportGenerator>();
            //services.AddHttpClient<BossaWebsiteClient>();


            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return services;
        }

        internal static IServiceCollection AddPolishTreasuryBondsAccountStateFileParsers(this IServiceCollection services)
        {
            var parserTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IPolishTreasuryBondsAccountStateFileParser)) && !q.IsAbstract);

            foreach (var parserType in parserTypes)
            {
                services.AddTransient(typeof(IPolishTreasuryBondsAccountStateFileParser), parserType);
            }

            return services;
        }
    }
}
