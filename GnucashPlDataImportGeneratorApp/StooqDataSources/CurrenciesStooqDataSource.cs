using GnuCash.CommodityPriceImportGenerator;
using GnuCash.DataModel.Dtos;
using StooqWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnucashPlDataImportGeneratorApp.StooqDataSources
{
    public class CurrenciesStooqDataSource : IPriceDataSourceForCommodityNamespace
    {
        private readonly StooqWebsiteClient _client;
        public CurrenciesStooqDataSource(StooqWebsiteClient client)
        {
            _client = client;
        }

        public async Task<Dictionary<string, List<CommodityPriceData>>> GetPricesData(IEnumerable<LastPriceDateDto> lastPricesData, string baseCurrency)
        {
            var result = new Dictionary<string, List<CommodityPriceData>>();


            foreach (var lastPriceData in lastPricesData)
            {
                var data = await _client.GetData(lastPriceData.Mnemonic + baseCurrency, lastPriceData.Date, DateTime.Today.AddDays(-1));

                result.Add(lastPriceData.Mnemonic, data.Data.Select(q => new CommodityPriceData()
                {
                    Date = q.Date,
                    Mnemonic = lastPriceData.Mnemonic,
                    Price = q.ClosingPrice
                }).ToList());
            }

            return result;
        }
    }
}
