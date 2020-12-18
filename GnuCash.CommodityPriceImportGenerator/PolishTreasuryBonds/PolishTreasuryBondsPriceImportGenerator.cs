using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using CsvHelper;
using GnuCash.CommodityPriceImportGenerator.Configuration;
using Microsoft.Extensions.Options;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    internal class PolishTreasuryBondsPriceImportGenerator : IPolishTreasuryBondsPriceImportGenerator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PolishTreasuryBondsPriceImportGeneratorSettings _settings;

        public PolishTreasuryBondsPriceImportGenerator(IServiceProvider serviceProvider,
            IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task GenerateImport(string filename)
        {
            filename ??= string.Empty;

            var bondsAccountStateFileInfo = new FileInfo(filename);

            if (!bondsAccountStateFileInfo.Exists)
                throw new InvalidOperationException($"Plik {filename} nie istnieje");

            var parser = _serviceProvider.GetServices<IPolishTreasuryBondsAccountStateFileParser>().FirstOrDefault(q => q.IsExtensionProcessable(bondsAccountStateFileInfo.Extension));

            if (parser == null)
                throw new InvalidOperationException($"Nie można odnaleźć parsera dla pliku {filename}");

            var fileRecords = await parser.ParseFile(filename);

            var groupedFileRecords = fileRecords.GroupBy(q => q.EmissionCode).Select(q => new { EmissionCode = q.Key, BondsCount = q.Sum(r => r.AvailableBonds + r.BlockedBonds), BondsCurrentValue = q.Sum(r => r.CurrentValue) });


            var importRows = new List<CommodityPriceImportFileRow>();

            importRows.AddRange(groupedFileRecords.Select(r => new CommodityPriceImportFileRow()
            {
                Date = bondsAccountStateFileInfo.LastWriteTime,
                Mnemonic = r.EmissionCode,
                Namespace = _settings.Namespace,
                Price = Math.Round(r.BondsCurrentValue / r.BondsCount, 2),
                BaseCurrency = _settings.BaseCurrency
            }));

            using var writer = new StreamWriter(_settings.GeneratedFilename);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(importRows);
        }
    }
}
