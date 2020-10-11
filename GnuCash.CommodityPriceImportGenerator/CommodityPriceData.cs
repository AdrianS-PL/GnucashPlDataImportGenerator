using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator
{
    public class CommodityPriceData
    {
        public string Mnemonic { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
