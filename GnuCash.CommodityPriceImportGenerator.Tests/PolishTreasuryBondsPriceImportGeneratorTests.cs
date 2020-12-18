using FakeItEasy;
using GnuCash.CommodityPriceImportGenerator.Configuration;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.Tests
{
    [TestClass]
    public class PolishTreasuryBondsPriceImportGeneratorTests : IDisposable
    {
        private const string generatedFileName = "polish_treasury_bonds_prices_import.csv";

        private bool disposedValue;
        private readonly string existingFilePath;

        public PolishTreasuryBondsPriceImportGeneratorTests()
        {
            existingFilePath = GetExistingFilePath();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
        public async Task Should_ThrowInvalidOperationException_When_FileDoesNotExist()
        {
            var settings = new PolishTreasuryBondsPriceImportGeneratorSettings() { };

            var fakeSettingsMonitor = A.Fake<IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings>>();
            A.CallTo(() => fakeSettingsMonitor.CurrentValue).Returns(settings);
            
            var generator = new PolishTreasuryBondsPriceImportGenerator(null, fakeSettingsMonitor);

            await generator.GenerateImport("not_existing_file.nef");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), AllowDerivedTypes = false)]
        public async Task Should_ThrowInvalidOperationException_When_NoParserFound()
        {
            var settings = new PolishTreasuryBondsPriceImportGeneratorSettings() { };

            var fakeSettingsMonitor = A.Fake<IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings>>();
            A.CallTo(() => fakeSettingsMonitor.CurrentValue).Returns(settings);

            var generator = new PolishTreasuryBondsPriceImportGenerator(GetParsersProvider(new List<Type>()), fakeSettingsMonitor);

            await generator.GenerateImport(existingFilePath);
        }

        [TestMethod]
        public async Task Should_CreateCsvFileWithNoRows_When_ParserReturnedEmptyList()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
";

            var settings = new PolishTreasuryBondsPriceImportGeneratorSettings() { GeneratedFilename = generatedFileName };

            var fakeParser = A.Fake<IPolishTreasuryBondsAccountStateFileParser>();
            A.CallTo(() => fakeParser.IsExtensionProcessable(A<string>._)).Returns(true);
            A.CallTo(() => fakeParser.ParseFile(A<string>._)).Returns(new List<PolishTreasuryBondsAccountStateFileRecord>());

            var fakeSettingsMonitor = A.Fake<IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings>>();
            A.CallTo(() => fakeSettingsMonitor.CurrentValue).Returns(settings);

            var generator = new PolishTreasuryBondsPriceImportGenerator(GetParsersProvider(new [] { fakeParser }), fakeSettingsMonitor);

            await generator.GenerateImport(existingFilePath);

            Assert.IsTrue(File.Exists(settings.GeneratedFilename));
            Assert.AreEqual(expectedFileContent, File.ReadAllText(settings.GeneratedFilename));
        }

        [TestMethod]
        public async Task Should_CreateValidCsvFile_When_ParserReturnsValidData()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Pañstwa,PLN,102,2000-02-17
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

            await TestFileGeneration("PLN", "Obligacje Skarbu Pañstwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_TakeBothIntoAccount_When_AvailableAndBlockedBondsAreNotZero()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Pañstwa,PLN,102,2000-02-17
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

            await TestFileGeneration("PLN", "Obligacje Skarbu Pañstwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_RoundPriceToTwoDecimals()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Pañstwa,PLN,33.33,2000-02-17
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

            await TestFileGeneration("PLN", "Obligacje Skarbu Pañstwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        [TestMethod]
        public async Task Should_GroupByEmissionCode()
        {
            string expectedFileContent =
                @"Mnemonic,Namespace,BaseCurrency,Price,Date
COI1024,Obligacje Skarbu Pañstwa,PLN,50,2000-02-17
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

            await TestFileGeneration("PLN", "Obligacje Skarbu Pañstwa", expectedFileContent, new DateTime(2000, 2, 17), data);
        }

        private async Task TestFileGeneration(string currency, string bondNamespace, string expectedFileContent, DateTime lastWriteTime,
            List<PolishTreasuryBondsAccountStateFileRecord> data)
        {
            var settings = new PolishTreasuryBondsPriceImportGeneratorSettings() { GeneratedFilename = generatedFileName, 
                BaseCurrency = currency, Namespace = bondNamespace };
            File.SetLastWriteTime(existingFilePath, lastWriteTime);

            var fakeParser = A.Fake<IPolishTreasuryBondsAccountStateFileParser>();
            A.CallTo(() => fakeParser.IsExtensionProcessable(A<string>._)).Returns(true);
            A.CallTo(() => fakeParser.ParseFile(A<string>._)).Returns(data);

            var fakeSettingsMonitor = A.Fake<IOptionsMonitor<PolishTreasuryBondsPriceImportGeneratorSettings>>();
            A.CallTo(() => fakeSettingsMonitor.CurrentValue).Returns(settings);

            var generator = new PolishTreasuryBondsPriceImportGenerator(GetParsersProvider(new[] { fakeParser }), fakeSettingsMonitor);            

            await generator.GenerateImport(existingFilePath);            

            Assert.IsTrue(File.Exists(settings.GeneratedFilename));
            Assert.AreEqual(expectedFileContent, File.ReadAllText(settings.GeneratedFilename));
        }

        private IServiceProvider GetParsersProvider(IEnumerable<IPolishTreasuryBondsAccountStateFileParser> instances)
        {
            var services = new ServiceCollection();

            foreach (var instance in instances)
                services.AddSingleton(instance);

            return services.BuildServiceProvider();
        }

        private IServiceProvider GetParsersProvider(List<Type> types)
        {
            var services = new ServiceCollection();
            foreach (var type in types)
                services.AddTransient(typeof(IPolishTreasuryBondsAccountStateFileParser), type);

            return services.BuildServiceProvider();
        }

        private string GetExistingFilePath()
        {
            return Path.GetTempFileName();
        }

        private void WriteResourceToFile(string path, string resource)
        {
            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(resource);

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            File.WriteAllBytes(path, buffer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                File.Delete(existingFilePath);
                if(File.Exists(generatedFileName))
                    File.Delete(generatedFileName);

                disposedValue = true;
            }
        }

        ~PolishTreasuryBondsPriceImportGeneratorTests()
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
