using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BossaWebsite.Model.FileRecords
{
    public class BossaCurrentNbpCurrenciesDataFileRecord
    {
        [Index(0)]
        public string Currency { get; set; }

        [Index(1)]
        [Format("yyyyMMdd")]
        public DateTime Date { get; set; }

        [Index(2)]
        public decimal Price { get; set; }
    }
}
