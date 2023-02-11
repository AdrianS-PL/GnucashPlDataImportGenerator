using System.Diagnostics.CodeAnalysis;

namespace GnuCash.CommodityPriceImportGenerator.Configuration;

[ExcludeFromCodeCoverage]
internal class CurrencyOverrideConfiguration
{
    public string Mnemonic { get; set; }
    public string Currency { get; set; }
}
