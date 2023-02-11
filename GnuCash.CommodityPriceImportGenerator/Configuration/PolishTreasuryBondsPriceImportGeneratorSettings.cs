using System.Diagnostics.CodeAnalysis;

namespace GnuCash.CommodityPriceImportGenerator.Configuration;

[ExcludeFromCodeCoverage]
public class PolishTreasuryBondsPriceImportGeneratorSettings
{
    public string Namespace { get; set; }
    public string GeneratedFilename { get; set; }
    public string BaseCurrency { get; set; }
}
