using FakeItEasy;
using GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.Tests
{
    [TestClass]
    public class PolishTreasuryBondsAccountStateFilesParserTests
    {

        [TestMethod]
        public async Task Should_ReturnEmptyResult_When_NoFilesGiven()
        {
            var filesParser = new PolishTreasuryBondsAccountStateFilesParser(GetParsersProvider(new List<Type>()));

            var result = await filesParser.ParseOperationsFiles(Array.Empty<string>());

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.LoadedFileRecords.Count);
            Assert.AreEqual(0, result.ParsingResults.Count);
        }

        [TestMethod]
        public async Task Should_ReturnErrorResult_When_NoParserWasFound()
        {
            var filesParser = new PolishTreasuryBondsAccountStateFilesParser(GetParsersProvider(new List<Type>()));

            var result = await filesParser.ParseOperationsFiles(new string[] { "test.xls" });

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.LoadedFileRecords.Count);
            Assert.AreEqual(1, result.ParsingResults.Count);
            Assert.IsTrue(result.ParsingResults[0].IsError);
            Assert.IsNull(result.ParsingResults[0].Exception);
            Assert.IsNull(result.ParsingResults[0].FileData);
            Assert.AreEqual("test.xls", result.ParsingResults[0].Pathname);
        }

        [TestMethod]
        public async Task Should_ReturnOkResult_When_FileParserReturnsValidResult()
        {
            var data = new List<PolishTreasuryBondsAccountStateFileRecord>() { new PolishTreasuryBondsAccountStateFileRecord() };

            var fakeParser = A.Fake<IPolishTreasuryBondsAccountStateFileParser>();
            A.CallTo(() => fakeParser.IsExtensionProcessable(A<string>._)).Returns(false);
            A.CallTo(() => fakeParser.IsExtensionProcessable(".xls")).Returns(true);
            A.CallTo(() => fakeParser.ParseFile(A<string>._)).Returns(data);

            var filesParser = new PolishTreasuryBondsAccountStateFilesParser(GetParsersProvider(new List<IPolishTreasuryBondsAccountStateFileParser>() { fakeParser }));

            var result = await filesParser.ParseOperationsFiles(new string[] { "test.xls" });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.LoadedFileRecords.Count);
            Assert.AreEqual(1, result.ParsingResults.Count);
            Assert.IsFalse(result.ParsingResults[0].IsError);
            Assert.IsNull(result.ParsingResults[0].Exception);
            CollectionAssert.AreEquivalent(data, result.ParsingResults[0].FileData.ToList());
            Assert.AreEqual("test.xls", result.ParsingResults[0].Pathname);
        }

        [TestMethod]
        public async Task Should_ReturnErrorResult_When_FileParserThrowsException()
        {
            var exception = new InvalidOperationException();

            var fakeParser = A.Fake<IPolishTreasuryBondsAccountStateFileParser>();
            A.CallTo(() => fakeParser.IsExtensionProcessable(A<string>._)).Returns(false);
            A.CallTo(() => fakeParser.IsExtensionProcessable(".xls")).Returns(true);
            A.CallTo(() => fakeParser.ParseFile(A<string>._)).Throws(exception);

            var filesParser = new PolishTreasuryBondsAccountStateFilesParser(GetParsersProvider(new List<IPolishTreasuryBondsAccountStateFileParser>() { fakeParser }));

            var result = await filesParser.ParseOperationsFiles(new string[] { "test.xls" });

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.LoadedFileRecords.Count);
            Assert.AreEqual(1, result.ParsingResults.Count);
            Assert.IsTrue(result.ParsingResults[0].IsError);
            Assert.AreEqual(exception, result.ParsingResults[0].Exception);
            Assert.IsNull(result.ParsingResults[0].FileData);
            Assert.AreEqual("test.xls", result.ParsingResults[0].Pathname);
        }

        [TestMethod]
        public async Task Should_ReturnOkResults_When_FileParserReturnsValidResults()
        {
            var data1 = new List<PolishTreasuryBondsAccountStateFileRecord>() { new PolishTreasuryBondsAccountStateFileRecord() };
            var data2 = new List<PolishTreasuryBondsAccountStateFileRecord>() { new PolishTreasuryBondsAccountStateFileRecord() };

            var fakeParser = A.Fake<IPolishTreasuryBondsAccountStateFileParser>();
            A.CallTo(() => fakeParser.IsExtensionProcessable(A<string>._)).Returns(false);
            A.CallTo(() => fakeParser.IsExtensionProcessable(".xls")).Returns(true);
            A.CallTo(() => fakeParser.ParseFile("test1.xls")).Returns(data1);
            A.CallTo(() => fakeParser.ParseFile("test2.xls")).Returns(data2);

            var filesParser = new PolishTreasuryBondsAccountStateFilesParser(GetParsersProvider(new List<IPolishTreasuryBondsAccountStateFileParser>() { fakeParser }));

            var result = await filesParser.ParseOperationsFiles(new string[] { "test1.xls" , "test2.xls" });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.LoadedFileRecords.Count);
            Assert.AreEqual(2, result.ParsingResults.Count);
            Assert.IsFalse(result.ParsingResults[0].IsError);
            Assert.IsNull(result.ParsingResults[0].Exception);
            CollectionAssert.AreEquivalent(data1, result.ParsingResults[0].FileData.ToList());
            Assert.AreEqual("test1.xls", result.ParsingResults[0].Pathname);
            Assert.IsFalse(result.ParsingResults[1].IsError);
            Assert.IsNull(result.ParsingResults[1].Exception);
            CollectionAssert.AreEquivalent(data2, result.ParsingResults[1].FileData.ToList());
            Assert.AreEqual("test2.xls", result.ParsingResults[1].Pathname);

            var allData = data1.ToList();
            allData.AddRange(data2);
            CollectionAssert.AreEquivalent(allData, result.LoadedFileRecords);
        }

        private static IServiceProvider GetParsersProvider(IEnumerable<IPolishTreasuryBondsAccountStateFileParser> instances)
        {
            var services = new ServiceCollection();

            foreach (var instance in instances)
                services.AddSingleton(instance);

            return services.BuildServiceProvider();
        }

        private static IServiceProvider GetParsersProvider(List<Type> types)
        {
            var services = new ServiceCollection();
            foreach (var type in types)
                services.AddTransient(typeof(IPolishTreasuryBondsAccountStateFileParser), type);

            return services.BuildServiceProvider();
        }
    }
}
