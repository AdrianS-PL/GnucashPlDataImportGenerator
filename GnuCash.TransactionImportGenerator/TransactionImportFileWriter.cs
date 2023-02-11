using CsvHelper;
using GnuCash.TransactionImportGenerator.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator
{
    public static class TransactionImportFileWriter
    {
        public static async Task GenerateReport(List<TransactionImportFileRow> rows)
        {
            using var writer = new StreamWriter("transaction_import.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync(rows);
        }
    }
}
