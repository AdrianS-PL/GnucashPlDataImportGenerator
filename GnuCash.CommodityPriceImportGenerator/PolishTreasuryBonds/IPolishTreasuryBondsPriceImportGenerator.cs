using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    public interface IPolishTreasuryBondsPriceImportGenerator
    {
        Task GenerateImport(string filename);
    }
}
