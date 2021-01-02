using GnuCash.DataModel.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator
{
    public interface IPriceDataSourceForCommodityNamespace
    {
        Task<Dictionary<string, List<CommodityPriceData>>> GetPricesData(IEnumerable<LastPriceDateDto> lastPricesData, string baseCurrency);
    }
}
