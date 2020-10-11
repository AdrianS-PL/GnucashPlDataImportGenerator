using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Parsers.IngCsvOperationsFile.DataContract
{
    [ExcludeFromCodeCoverage]
    class OperationDataLine
    {
        [Index(0)]
        [Format("yyyy-MM-dd")]
        public DateTime TransactionDate { get; set; }

        [Index(1)]
        [Format("yyyy-MM-dd")]
        public DateTime? BookingDate { get; set; }

        [Index(2)]
        public string Contractor { get; set; }

        [Index(3)]
        public string Title { get; set; }

        [Index(4)]
        public string OppositeAccountNo { get; set; }

        [Index(5)]
        public string BankName { get; set; }

        [Index(6)]
        public string Details { get; set; }

        [Index(7)]
        public string TransactionNo { get; set; }

        [Index(8)]
        public decimal? AmountInAccountCurrency { get; set; }

        [Index(9)]
        public string Currency { get; set; }
    }
}
