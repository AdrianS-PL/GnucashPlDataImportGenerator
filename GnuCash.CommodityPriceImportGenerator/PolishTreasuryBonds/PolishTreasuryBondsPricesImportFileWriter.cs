using CsvHelper;
using GnuCash.CommodityPriceImportGenerator.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    public class PolishTreasuryBondsPricesImportFileWriter
    {
        private readonly PolishTreasuryBondsPriceImportGeneratorSettings _settings;
        public PolishTreasuryBondsPricesImportFileWriter(IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings> settings)
        {
            _settings = settings.CurrentValue ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task WriteImportFile(List<PolishTreasuryBondsAccountStateFileRecord> accountStateFilesRecords, DateTime priceDate)
        {
            var groupedFileRecords = accountStateFilesRecords.GroupBy(q => q.EmissionCode)
                .Select(q => new { 
                    EmissionCode = q.Key, 
                    BondsCount = q.Sum(r => r.AvailableBonds + r.BlockedBonds), 
                    BondsCurrentValue = q.Sum(r => r.CurrentValue) 
                });


            var importRows = new List<CommodityPriceImportFileRow>();

            importRows.AddRange(groupedFileRecords.Select(r => new CommodityPriceImportFileRow()
            {
                Date = priceDate,
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
