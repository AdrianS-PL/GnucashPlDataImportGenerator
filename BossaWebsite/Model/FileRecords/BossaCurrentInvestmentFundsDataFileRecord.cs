using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BossaWebsite.Model.FileRecords
{
    public class BossaCurrentInvestmentFundsDataFileRecord
    {
        [Index(0)]
        public string FundSymbol { get; set; }

        [Index(1)]
        [Format("yyyyMMdd")]
        public DateTime Date { get; set; }

        [Index(2)]
        public decimal Price { get; set; }
    }
}
