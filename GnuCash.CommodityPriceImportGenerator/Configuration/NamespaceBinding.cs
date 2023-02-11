using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.Configuration
{
    internal class NamespaceBinding
    {

        public NamespaceBinding()
        {
            CurrencyOverrides = Array.Empty<CurrencyOverrideConfiguration>();
        }

        public string Namespace { get; set; }
        public string DataSourceTypeName { get; set; }
        public CurrencyOverrideConfiguration[] CurrencyOverrides { get; set; }
    }
}
