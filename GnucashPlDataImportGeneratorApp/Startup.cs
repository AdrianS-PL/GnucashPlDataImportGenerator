using GnuCash.CommodityPriceImportGenerator;
using GnuCash.DataModel.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using GnuCash.TransactionImportGenerator;
using GnuCash.DataModel;
using StooqWebsite;
using GnucashPlDataImportGeneratorApp.StooqDataSources;

namespace GnucashPlDataImportGeneratorApp
{
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
}
