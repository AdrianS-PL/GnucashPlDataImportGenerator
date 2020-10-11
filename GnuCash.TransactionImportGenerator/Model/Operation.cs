using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class Operation
    {
        public string AccountCode { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
