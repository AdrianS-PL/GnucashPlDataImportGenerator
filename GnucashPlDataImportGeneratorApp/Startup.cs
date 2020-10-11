using GnucashPlDataImportGeneratorApp.BossaDataSources;
using BossaWebsite;
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
            services.AddBossaWebsiteClient(Configuration);
            services.AddCommodityPriceImportGenerator(Configuration);
            services.AddTransactionImportGenerator(Configuration);

            services.AddTransient<CurrenciesBossaDataSource, CurrenciesBossaDataSource>();
            services.AddTransient<InvestmentFundsBossaDataSource, InvestmentFundsBossaDataSource>();


            return services.BuildServiceProvider();
        }

        private static string GetBasePath()
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            return Path.GetDirectoryName(processModule?.FileName);
        }
    }
}
