using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    internal interface IPolishTreasuryBondsAccountStateFileParser
    {
        bool IsExtensionProcessable(string extension);
        Task<IList<PolishTreasuryBondsAccountStateFileRecord>> ParseFile(string filename);
    }
}
