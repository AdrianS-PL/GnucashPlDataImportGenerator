using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using GnuCash.TransactionImportGenerator.Parsers;
using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlCardOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.PkoBpXmlOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.PocztowyXmlOperationsFile;
using GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;

[assembly: InternalsVisibleTo("GnuCash.TransactionImportGenerator.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace GnuCash.TransactionImportGenerator
{
    [ExcludeFromCodeCoverage]
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddTransactionImportGenerator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<OperationFilesParser, OperationFilesParser>();
            services.AddTransient<TransactionImportFileDataGenerator, TransactionImportFileDataGenerator>();
            services.AddOperationsFileParsers();
            services.AddTransient<IOperationTransferAccountPredictor, MlNetOperationTransferAccountPredictor>();
            services.AddTransient<MlNetPreSplitMappingProfile, MlNetPreSplitMappingProfile>();
            services.AddTransient<ITrainingAndTestDataSplitStrategy<MlNetPreSplitPredictionInputRow> , LastMonthDataAsTestDataSplitStrategy>();

            return services;
        }

        internal static IServiceCollection AddOperationsFileParsers(this IServiceCollection services)
        {
            var parserTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IOperationsFileParser)) && !q.IsAbstract);

            foreach (var parserType in parserTypes)
            {
                services.AddTransient(typeof(IOperationsFileParser), parserType);
            }

            return services;
        }
    }
}
