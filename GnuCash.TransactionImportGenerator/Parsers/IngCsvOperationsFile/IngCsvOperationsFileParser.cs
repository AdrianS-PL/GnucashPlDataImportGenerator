using CsvHelper;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.Parsers;
using GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile.DataContract;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile
{
    class IngCsvOperationsFileParser : IOperationsFileParser
    {
        public Encoding FileEncoding => CodePagesEncodingProvider.Instance.GetEncoding(1250);

        public bool CanParse(string fileContent)
        {
            try
            {
                List<string> lines = ReadAsLines(fileContent);

                if (!lines[0].StartsWith("\"Lista transakcji\""))
                    return false;

                if (!lines[0].Contains("ing", StringComparison.InvariantCultureIgnoreCase))
                    return false;

                int accountsHeaderLineIndex = GetAccountsHeaderLineIndex(lines);

                if (string.IsNullOrWhiteSpace(lines[accountsHeaderLineIndex + 1]))
                    return false;

                if (!string.IsNullOrWhiteSpace(lines[accountsHeaderLineIndex + 2]))
                    return false;

                GetOperationsDataStartLineIndex(lines, accountsHeaderLineIndex);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Operation> MapToOperationsFile(string fileContent)
        {
            List<string> lines = ReadAsLines(fileContent);

            int accountsHeaderLineIndex = GetAccountsHeaderLineIndex(lines);

            int operationsDataStartLineIndex = GetOperationsDataStartLineIndex(lines, accountsHeaderLineIndex);

            int operationsDataEndLineIndex = GetOperationsDataEndLineIndex(lines, operationsDataStartLineIndex);

            string accountCode = GetAccountCode(lines[accountsHeaderLineIndex + 1]);

            var csvDataLines = lines.Skip(operationsDataStartLineIndex).Take(operationsDataEndLineIndex - operationsDataStartLineIndex + 1).ToList();

            string csvDataString = string.Join(Environment.NewLine, csvDataLines);

            var cultureInfo = CultureInfo.GetCultureInfo("pl");
            using var reader = new StringReader(csvDataString);
            using var csv = new CsvReader(reader, cultureInfo);

            csv.Configuration.HasHeaderRecord = true;
            csv.Configuration.Delimiter = ";";

            var fileRecords = csv.GetRecords<OperationDataLine>().ToList();

            var r = from f in fileRecords
                    where f.AmountInAccountCurrency.HasValue
                    select new Operation()
                    {
                        AccountCode = accountCode,
                        Amount = f.AmountInAccountCurrency.Value,
                        Description = "Dane kontrahenta: " + f.Contractor + " Tytuł: " + f.Title + " Rachunek przeciwstawny: " + f.OppositeAccountNo + " " + f.Details,
                        Currency = f.Currency,
                        Date = new DateTime[] { f.TransactionDate, f.BookingDate ?? DateTime.MaxValue }.Min()
                    };

            return r.ToList();
        }

        private string GetAccountCode(string accountLine)
        {
            var cultureInfo = CultureInfo.InvariantCulture;


            using var reader = new StringReader(accountLine);
            using var csv = new CsvReader(reader, cultureInfo);

            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.Delimiter = ";";

            return csv.GetRecords<AccountDataLine>().Single().AccountCode;
        }

        private int GetAccountsHeaderLineIndex(List<string> lines)
        {
            string accountsHeaderLine = lines.First(q => q.StartsWith("\"Wybrane rachunki:\""));
            return lines.IndexOf(accountsHeaderLine);
        }

        private int GetOperationsDataStartLineIndex(List<string> lines, int accountsHeaderLineIndex)
        {
            string operationsDataStartLine = lines.Skip(accountsHeaderLineIndex + 1).First(q => q.StartsWith("\"Data transakcji\""));
            return lines.IndexOf(operationsDataStartLine);
        }

        private int GetOperationsDataEndLineIndex(List<string> lines, int operationsDataStartLineIndex)
        {
            return lines.FindIndex(operationsDataStartLineIndex, q => string.IsNullOrWhiteSpace(q)) - 1;
        }

        private List<string> ReadAsLines(string ingCsv)
        {
            var lines = new List<string>();
            using var reader = new StringReader(ingCsv);
            string line = reader.ReadLine();

            while (line != null)
            {
                lines.Add(line);
                line = reader.ReadLine();
            }

            return lines;
        }
    }
}
