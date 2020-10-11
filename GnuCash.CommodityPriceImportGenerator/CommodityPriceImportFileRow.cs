using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator
{
    class CommodityPriceImportFileRow
    {
        [Index(0)]
        public string Mnemonic { get; set; }

        [Index(1)]
        public string Namespace { get; set; }

        [Index(2)]
        public string BaseCurrency { get; set; }

        [Index(3)]
        public decimal Price { get; set; }

        [Index(4)]
        [Format("yyyy-MM-dd")]
        public DateTime Date { get; set; }
    }
}
