using System.Diagnostics.CodeAnalysis;

namespace GnuCash.CommodityPriceImportGenerator.Configuration;

[ExcludeFromCodeCoverage]
internal class CommodityPriceImportGeneratorSettings
{
    public NamespaceBinding[] Bindings { get; set; }

    public string GeneratedFilename { get; set; }

    public string BaseCurrency { get; set; }
}
