using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.Configuration
{
    internal class NamespaceBinding
    {

        public NamespaceBinding()
        {
            CurrencyOverrides = new CurrencyOverrideConfiguration[0];
        }

        public string Namespace { get; set; }
        public string DataSourceTypeName { get; set; }
        public CurrencyOverrideConfiguration[] CurrencyOverrides { get; set; }
    }
}
