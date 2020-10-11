using CsvHelper;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.Parsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator
{
    public class TransactionImportFileWriter
    {
        public TransactionImportFileWriter() { }

        public async Task GenerateReport(List<TransactionImportFileRow> rows)
        {
            using (var writer = new StreamWriter("transaction_import.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                await csv.WriteRecordsAsync(rows);
            }
        }
    }
}
