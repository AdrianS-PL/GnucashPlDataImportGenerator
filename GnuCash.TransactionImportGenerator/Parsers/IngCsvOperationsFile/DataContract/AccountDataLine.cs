using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile.DataContract
{
    [ExcludeFromCodeCoverage]
    class AccountDataLine
    {
        [Index(0)]
        public string AccountName { get; set; }

        [Index(2)]
        public string AccountCode { get; set; }
    }
}
