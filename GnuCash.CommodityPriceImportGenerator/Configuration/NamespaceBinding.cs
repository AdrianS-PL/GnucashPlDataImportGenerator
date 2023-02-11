using System;
using System.Diagnostics.CodeAnalysis;

namespace GnuCash.CommodityPriceImportGenerator.Configuration;

[ExcludeFromCodeCoverage]
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
