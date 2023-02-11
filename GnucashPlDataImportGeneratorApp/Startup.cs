using GnuCash.CommodityPriceImportGenerator;
using GnuCash.DataModel;
using GnuCash.TransactionImportGenerator;
using GnucashPlDataImportGeneratorApp.StooqDataSources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StooqWebsite;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace GnucashPlDataImportGeneratorApp;

[ExcludeFromCodeCoverage]

static class Startup
{
    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(GetBasePath())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        services.AddGnuCashDataModel(Configuration);
        services.AddStooqWebsiteClient(Configuration);
        services.AddCommodityPriceImportGenerator(Configuration);
        services.AddTransactionImportGenerator(Configuration);

        services.AddTransient<CurrenciesStooqDataSource, CurrenciesStooqDataSource>();
        services.AddTransient<InvestmentFundsStooqDataSource, InvestmentFundsStooqDataSource>();


        return services.BuildServiceProvider();
    }

    private static string GetBasePath()
    {
        using var processModule = Process.GetCurrentProcess().MainModule;
        return Path.GetDirectoryName(processModule?.FileName);
    }
}
