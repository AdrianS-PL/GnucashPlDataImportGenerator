using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.Model
{
    public class OperationPair
    {
        public string AccountCode1 => Operation1.AccountCode;
        public string AccountCode2 => Operation2.AccountCode;
        public DateTime Date1 => Operation1.Date;
        public DateTime Date2 => Operation2.Date;
        public decimal Amount1 => Operation1.Amount;
        public decimal Amount2 => Operation2.Amount;
        public string Currency1 => Operation1.Currency;
        public string Currency2 => Operation2.Currency;
        public string Description1 => Operation1.Description;
        public string Description2 => Operation2.Description;

        public Operation Operation1 { get; set; }
        public Operation Operation2 { get; set; }
    }
}
