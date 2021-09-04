using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.Tests
{
    [TestClass]
    public class BaseCurrencyPriceCalculatorForImportRowsTests
    {
        [TestMethod]
        public void Should_ReturnEmpty_When_InputRowsIsEmpty()
        {
            var result = BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(new List<CommodityPriceImportFileRow>(), "PLN").ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_ThrowArgumentNullException_When_InputRowsIsNull()
        {
            BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(null, "PLN").ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_ThrowArgumentNullException_When_BaseCurrencyIsNull()
        {
            BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(new List<CommodityPriceImportFileRow>(), null).ToList();
        }

        [TestMethod]
        public void Should_OmitCalculation_When_BaseCurrencyRateForDateNotFound()
        {
            var input = new List<CommodityPriceImportFileRow>
            {
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = "PLN",
                    Date = new DateTime(2000, 1, 3),
                    Mnemonic = "USD",
                    Namespace = "test",
                    Price = 4.0m
                },
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = "PLN",
                    Date = new DateTime(2000, 1, 1),
                    Mnemonic = "EUR",
                    Namespace = "test",
                    Price = 4.0m
                },
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = "USD",
                    Date = new DateTime(2000, 1, 1),
                    Mnemonic = "IGLN",
                    Namespace = "test",
                    Price = 10.0m
                }
            };

            var result = BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(input, "PLN").ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Should_ReturnCalculatedPrice_When_InputDataIsCorrect()
        {
            string baseCurrency = "PLN";
            string assetCurrency = "USD";
            string assetMnemonic = "IGLN";
            decimal baseCurrencyRate1 = 4.0m;
            decimal assetPrice1 = 10.0m;
            decimal baseCurrencyRate2 = 5.0m;
            decimal assetPrice2 = 20.0m;

            var input = new List<CommodityPriceImportFileRow>
            {
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = baseCurrency,
                    Date = new DateTime(2000, 1, 1),
                    Mnemonic = assetCurrency,
                    Namespace = "test",
                    Price = baseCurrencyRate1
                },
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = assetCurrency,
                    Date = new DateTime(2000, 1, 1),
                    Mnemonic = assetMnemonic,
                    Namespace = "test",
                    Price = assetPrice1
                },
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = baseCurrency,
                    Date = new DateTime(2000, 1, 2),
                    Mnemonic = assetCurrency,
                    Namespace = "test",
                    Price = baseCurrencyRate2
                },
                new CommodityPriceImportFileRow
                {
                    BaseCurrency = assetCurrency,
                    Date = new DateTime(2000, 1, 2),
                    Mnemonic = assetMnemonic,
                    Namespace = "test",
                    Price = assetPrice2
                },
            };

            var result = BaseCurrencyPriceCalculatorForImportRows.CalculateBaseCurrencyPricesRows(input, baseCurrency).ToList();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2);

            var firstCalculatedPrice = result.Single(q => q.Date == new DateTime(2000, 1, 1) && q.Mnemonic == assetMnemonic && q.BaseCurrency == baseCurrency);
            Assert.AreEqual(assetPrice1 * baseCurrencyRate1, firstCalculatedPrice.Price);
            Assert.AreEqual("test", firstCalculatedPrice.Namespace);

            var secondCalculatedPrice = result.Single(q => q.Date == new DateTime(2000, 1, 2) && q.Mnemonic == assetMnemonic && q.BaseCurrency == baseCurrency);
            Assert.AreEqual(assetPrice2 * baseCurrencyRate2, secondCalculatedPrice.Price);
            Assert.AreEqual("test", secondCalculatedPrice.Namespace);
        }
    }
}
