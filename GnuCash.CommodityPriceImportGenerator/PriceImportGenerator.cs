using GnuCash.CommodityPriceImportGenerator.Configuration;
using GnuCash.DataModel.DatabaseModel;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using GnuCash.DataModel.Queries;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace GnuCash.CommodityPriceImportGenerator
{
    internal class PriceImportGenerator : IPriceImportGenerator
    {
		private readonly CommodityPriceImportGeneratorSettings _settings;
		private readonly GnuCashContext _context; //dispose?
		private readonly IServiceProvider _serviceProvider;

		public PriceImportGenerator(IServiceProvider serviceProvider, IOptionsMonitor<CommodityPriceImportGeneratorSettings> settings, GnuCashContext context)
		{
			_settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
			_settings.Bindings ??= new NamespaceBinding[] { };
			_context = context;
			_serviceProvider = serviceProvider;
		}

		public async Task GenerateImport()
        {
			var importRows = new List<CommodityPriceImportFileRow>();

			var lastPriceDatesByNamespace = (await _context.Set<Price>().GetLastCommodityPricesDatesAsync())
				.GroupBy(q => q.Namespace).ToDictionary(q => q.Key, r => r.ToList());

			foreach(var lastPriceDatesInNamespace in lastPriceDatesByNamespace)
            {
				var namespaceBinding = _settings.Bindings.SingleOrDefault(q => q.Namespace == lastPriceDatesInNamespace.Key);
				string dataSourceTypeName = namespaceBinding?.DataSourceTypeName;
				
				if (dataSourceTypeName == null)
					continue;

				var dataSourceType = Type.GetType(dataSourceTypeName);

				if (dataSourceType == null)
					continue;

				var instantiatedObject = _serviceProvider.GetService(dataSourceType) as IPriceDataSourceForCommodityNamespace;

				var priceDataForNamespace = await instantiatedObject.GetPricesData(lastPriceDatesInNamespace.Value, _settings.BaseCurrency);

				foreach(var lastPriceDate in lastPriceDatesInNamespace.Value)
                {
					string currencyOverride = namespaceBinding.CurrencyOverrides.FirstOrDefault(q => q.Mnemonic == lastPriceDate.Mnemonic)?.Currency;

					importRows.AddRange(priceDataForNamespace[lastPriceDate.Mnemonic]
						.Where(q => q.Date > lastPriceDate.Date)
						.Select(r => new CommodityPriceImportFileRow() { 
							Date = r.Date, 
							Mnemonic = r.Mnemonic, 
							Namespace = lastPriceDatesInNamespace.Key, 
							Price = r.Price,
							BaseCurrency = currencyOverride ?? _settings.BaseCurrency
						}));
				}
			}

			var baseCurrencyPrices = BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(importRows, _settings.BaseCurrency).ToList();
			importRows.AddRange(baseCurrencyPrices);

			using (var writer = new StreamWriter(_settings.GeneratedFilename))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				await csv.WriteRecordsAsync(importRows);
			}
        }
    }
}
