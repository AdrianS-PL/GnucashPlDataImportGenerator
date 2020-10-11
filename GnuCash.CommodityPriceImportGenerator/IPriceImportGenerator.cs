using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator
{
    public interface IPriceImportGenerator
    {
        Task GenerateImport();
    }
}
