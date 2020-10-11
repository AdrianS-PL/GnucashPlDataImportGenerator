using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class TransactionImportFileRow
    {
        [Index(0)]
        [Format("yyyy-MM-dd")]
        public DateTime Date { get; set; }
        
        [Index(1)]
        public string Account { get; set; }
        
        [Index(2)]
        public string Description { get; set; }
        
        [Index(3)]
        public decimal Deposit { get; set; }
        
        [Index(4)]
        public string Memo { get; set; }
        
        [Index(5)]
        public string TransferAccount { get; set; }
        
        [Index(6)]
        public string TransferMemo { get; set; }
    }
}
