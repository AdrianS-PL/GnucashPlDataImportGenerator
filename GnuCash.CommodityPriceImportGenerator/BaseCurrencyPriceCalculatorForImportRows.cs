using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GnuCash.CommodityPriceImportGenerator
{
    internal class BaseCurrencyPriceCalculatorForImportRows
    {
        public static IEnumerable<CommodityPriceImportFileRow> CalculateBaseCurrencyPricesRows(IEnumerable<CommodityPriceImportFileRow> inputRows, string baseCurrency)
        {
            inputRows = inputRows ?? throw new ArgumentNullException(nameof(inputRows));
            baseCurrency = baseCurrency ?? throw new ArgumentNullException(nameof(baseCurrency));

            var rowsWithPriceNotInBaseCurrency = inputRows.Where(q => q.BaseCurrency != baseCurrency);

            foreach(var inputRow in rowsWithPriceNotInBaseCurrency)
            {
                var rowWithCurrencyRate = inputRows.FirstOrDefault(q => q.Mnemonic == inputRow.BaseCurrency && q.Date == inputRow.Date && q.BaseCurrency == baseCurrency);

                if(rowWithCurrencyRate != null)
                {
                    yield return new CommodityPriceImportFileRow()
                    {
                        Date = inputRow.Date,
                        Mnemonic = inputRow.Mnemonic,
                        Namespace = inputRow.Namespace,
                        Price = Math.Round(inputRow.Price * rowWithCurrencyRate.Price, 2, MidpointRounding.AwayFromZero),
                        BaseCurrency = baseCurrency
                    };
                }
            }
        }
    }
}
