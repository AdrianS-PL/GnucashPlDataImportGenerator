using FakeItEasy;
using GnuCash.CommodityPriceImportGenerator.Configuration;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.Tests
{
    [TestClass]
    public class PolishTreasuryBondsPricesImportFileWriterTests : IDisposable
    {
        private const string generatedFileName = "polish_treasury_bonds_prices_import.csv";
        private bool disposedValue;

        [TestMethod]
        public async Task Should_CreateValidCsvFile_When_ParserReturnsValidData()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Państwa,PLN,102,2000-02-17
";

            var data = new List<PolishTreasuryBondsAccountStateFileRecord>()
            {
                new PolishTreasuryBondsAccountStateFileRecord()
                {
                    AvailableBonds = 1,
                    BlockedBonds = 0,
                    CurrentValue = 102,
                    EmissionCode = "COI1024"
                }
            };

            await TestFileGeneration("PLN", "Obligacje Skarbu Państwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_TakeBothIntoAccount_When_AvailableAndBlockedBondsAreNotZero()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Państwa,PLN,102,2000-02-17
";

            var data = new List<PolishTreasuryBondsAccountStateFileRecord>()
            {
                new PolishTreasuryBondsAccountStateFileRecord()
                {
                    AvailableBonds = 1,
                    BlockedBonds = 1,
                    CurrentValue = 204,
                    EmissionCode = "COI1024"
                }
            };

            await TestFileGeneration("PLN", "Obligacje Skarbu Państwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_RoundPriceToTwoDecimals()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Państwa,PLN,33.33,2000-02-17
";

            var data = new List<PolishTreasuryBondsAccountStateFileRecord>()
            {
                new PolishTreasuryBondsAccountStateFileRecord()
                {
                    AvailableBonds = 3,
                    BlockedBonds = 0,
                    CurrentValue = 100,
                    EmissionCode = "COI1024"
                }
            };

            await TestFileGeneration("PLN", "Obligacje Skarbu Państwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_GroupByEmissionCode()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Państwa,PLN,50,2000-02-17
";

            var data = new List<PolishTreasuryBondsAccountStateFileRecord>()
            {
                new PolishTreasuryBondsAccountStateFileRecord()
                {
                    AvailableBonds = 3,
                    BlockedBonds = 0,
                    CurrentValue = 100,
                    EmissionCode = "COI1024"
                },
                new PolishTreasuryBondsAccountStateFileRecord()
                {
                    AvailableBonds = 3,
                    BlockedBonds = 0,
                    CurrentValue = 200,
                    EmissionCode = "COI1024"
                }
            };

            await TestFileGeneration("PLN", "Obligacje Skarbu Państwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        private static async Task TestFileGeneration(string currency, string bondNamespace, string expectedFileContent, DateTime priceDate,
            List<PolishTreasuryBondsAccountStateFileRecord> data)
        {
            var settings = new PolishTreasuryBondsPriceImportGeneratorSettings()
            {
                GeneratedFilename = generatedFileName,
                BaseCurrency = currency,
                Namespace = bondNamespace
            };

            var fakeSettingsMonitor = A.Fake<IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings>>();
            A.CallTo(() => fakeSettingsMonitor.CurrentValue).Returns(settings);

            var writer = new PolishTreasuryBondsPricesImportFileWriter(fakeSettingsMonitor);
            await writer.WriteImportFile(data, priceDate);

            Assert.IsTrue(File.Exists(settings.GeneratedFilename));
            Assert.AreEqual(expectedFileContent, File.ReadAllText(settings.GeneratedFilename));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (File.Exists(generatedFileName))
                    File.Delete(generatedFileName);

                disposedValue = true;
            }
        }

        ~PolishTreasuryBondsPricesImportFileWriterTests()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
