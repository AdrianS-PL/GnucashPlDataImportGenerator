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
    public class PolishTreasuryBondsAccountStateXlsFileParserTests : IDisposable
    {
        private bool disposedValue;
        private readonly string validExcelFilePath;
        private readonly string validIkeExcelFilePath;


        [AssemblyInitialize]
        public static void InitializeExcelDataReader(TestContext context)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        [TestMethod]
        public void Should_AcceptXlsExtension()
        {
            var parser = new PolishTreasuryBondsAccountStateXlsFileParser();

            Assert.IsTrue(parser.IsExtensionProcessable(".xls"));
            Assert.IsTrue(parser.IsExtensionProcessable(".XLS"));
            Assert.IsTrue(parser.IsExtensionProcessable(".Xls"));
            Assert.IsTrue(parser.IsExtensionProcessable("xls"));
            Assert.IsTrue(parser.IsExtensionProcessable("XLS"));
            Assert.IsTrue(parser.IsExtensionProcessable("Xls"));
            Assert.IsFalse(parser.IsExtensionProcessable("tmp"));
            Assert.IsFalse(parser.IsExtensionProcessable(".tmp"));
        }

        [TestMethod]
        public async Task Should_CorrectlyParseXlsFile_When_FileIsValid()
        {
            var parser = new PolishTreasuryBondsAccountStateXlsFileParser();
            var result = await parser.ParseFile(validExcelFilePath);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("COI1115", result[0].EmissionCode);
            Assert.AreEqual(3, result[0].AvailableBonds);
            Assert.AreEqual(0, result[0].BlockedBonds);
            Assert.AreEqual(300m, result[0].NominalValue);
            Assert.AreEqual(301.24m, result[0].CurrentValue);
            Assert.AreEqual(new DateTime(2020, 10, 3), result[0].BuyoutDate);
            Assert.AreEqual("COI0417", result[1].EmissionCode);
            Assert.AreEqual(2, result[1].AvailableBonds);
            Assert.AreEqual(0, result[1].BlockedBonds);
            Assert.AreEqual(200m, result[1].NominalValue);
            Assert.AreEqual(256.54m, result[1].CurrentValue);
            Assert.AreEqual(new DateTime(2019, 3, 4), result[1].BuyoutDate);
            Assert.AreEqual("COI0417", result[2].EmissionCode);
            Assert.AreEqual(5, result[2].AvailableBonds);
            Assert.AreEqual(1, result[2].BlockedBonds);
            Assert.AreEqual(600m, result[2].NominalValue);
            Assert.AreEqual(683.23m, result[2].CurrentValue);
            Assert.AreEqual(new DateTime(2019, 3, 5), result[2].BuyoutDate);
        }

        [TestMethod]
        public async Task Should_CorrectlyParseIkeXlsFile_When_FileIsValid()
        {
            var parser = new PolishTreasuryBondsAccountStateXlsFileParser();
            var result = await parser.ParseFile(validIkeExcelFilePath);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("EDO1230", result[0].EmissionCode);
            Assert.AreEqual(1, result[0].AvailableBonds);
            Assert.AreEqual(0, result[0].BlockedBonds);
            Assert.AreEqual(100m, result[0].NominalValue);
            Assert.AreEqual(100.51m, result[0].CurrentValue);
            Assert.AreEqual(new DateTime(2030, 12, 16), result[0].BuyoutDate);
            Assert.AreEqual("EDO0131", result[1].EmissionCode);
            Assert.AreEqual(3, result[1].AvailableBonds);
            Assert.AreEqual(0, result[1].BlockedBonds);
            Assert.AreEqual(300m, result[1].NominalValue);
            Assert.AreEqual(301.11m, result[1].CurrentValue);
            Assert.AreEqual(new DateTime(2031, 1, 16), result[1].BuyoutDate);
        }

        public PolishTreasuryBondsAccountStateXlsFileParserTests()
        {
            validExcelFilePath = GetValidExcelFilePath();
            validIkeExcelFilePath = GetValidIkeExcelFilePath();
        }

        private static string GetValidExcelFilePath()
        {
            string path = Path.GetTempFileName();
            WriteResourceToFile(path, "GnuCash.CommodityPriceImportGenerator.Tests.TestData.StanRachunkuRejestrowego_2020-09-30.xls");
            return path;
        }

        private static string GetValidIkeExcelFilePath()
        {
            string path = Path.GetTempFileName();
            WriteResourceToFile(path, "GnuCash.CommodityPriceImportGenerator.Tests.TestData.StanRachunkuIKE_2021-04-05.xls");
            return path;
        }

        private static void WriteResourceToFile(string path, string resource)
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
                File.Delete(validExcelFilePath);
                File.Delete(validIkeExcelFilePath);
                disposedValue = true;
            }
        }

        ~PolishTreasuryBondsAccountStateXlsFileParserTests()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
