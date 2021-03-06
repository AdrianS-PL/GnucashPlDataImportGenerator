﻿using BossaWebsite;
using GnuCash.CommodityPriceImportGenerator;
using GnuCash.DataModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnucashPlDataImportGeneratorApp.BossaDataSources
{
    public class InvestmentFundsBossaDataSource : IPriceDataSourceForCommodityNamespace
    {
        BossaWebsiteClient _client;
        public InvestmentFundsBossaDataSource(BossaWebsiteClient client)
        {
            _client = client;
        }

        public async Task<Dictionary<string, List<CommodityPriceData>>> GetPricesData(IEnumerable<LastPriceDateDto> lastPricesData, string baseCurrency)
        {
            var data = await _client.GetHistoricInvestmentFundsData();

            var result = new Dictionary<string, List<CommodityPriceData>>();

            foreach (var keyValuePair in data.Data)
            {
                result.Add(keyValuePair.Key, keyValuePair.Value.Select(q => new CommodityPriceData()
                {
                    Date = q.Date,
                    Mnemonic = q.FundSymbol,
                    Price = q.Price
                }).ToList());
            }

            return result;
        }
    }
}
