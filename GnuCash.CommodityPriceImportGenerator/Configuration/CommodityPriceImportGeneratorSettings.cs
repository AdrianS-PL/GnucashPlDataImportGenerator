using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.Configuration
{
    internal class CommodityPriceImportGeneratorSettings
    {
        public NamespaceBinding[] Bindings { get; set; }
        
        public string GeneratedFilename { get; set; }
        
        public string BaseCurrency { get; set; }
    }
}
