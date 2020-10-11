using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GnuCash.TransactionImportGenerator.Parsers;
using GnuCash.TransactionImportGenerator.Model;

namespace GnuCash.TransactionImportGenerator.Tests
{
    [TestClass]
    public class OperationFilesParserTests : IDisposable
    {
        public class FakeAlwaysCanParseUtf8Parser : IOperationsFileParser
        {
            public Encoding FileEncoding => Encoding.UTF8;

            public bool CanParse(string xml)
            {
                return true;
            }

            public List<Operation> MapToOperationsFile(string xml)
            {
                return new List<Operation>(){ new Operation() { Description = xml } };
            }
        }

        public class FakeAlwaysCanParseWindows1250Parser : IOperationsFileParser
        {
            public Encoding FileEncoding => CodePagesEncodingProvider.Instance.GetEncoding(1250);

            public bool CanParse(string xml)
            {
                return true;
            }

            public List<Operation> MapToOperationsFile(string xml)
            {
                return new List<Operation>() { new Operation() { Description = xml } };
            }
        }


        private bool disposedValue;
        private readonly string windows1250FilePath;
        private readonly string utf8FilePath;

        public OperationFilesParserTests()
        {
            windows1250FilePath = GetWindows1250FilePath();
            utf8FilePath = GetUtf8FilePath();
        }

        [TestMethod]
        public async Task Should_ReturnEmptyResult_When_InputIsNull()
        {
            var parser = new OperationFilesParser(null);

            var result = await parser.ParseOperationsFiles(null);

            Assert.AreEqual(0, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(0, result.ParsingResults.Count);
        }

        [TestMethod]
        public async Task Should_ReturnEmptyResult_When_InputIsEmpty()
        {
            var parser = new OperationFilesParser(null);

            var result = await parser.ParseOperationsFiles(new string[0]);

            Assert.AreEqual(0, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(0, result.ParsingResults.Count);
        }

        [TestMethod]
        public async Task Should_ReturnIsErrorResult_When_NoParsersAreDefined()
        {          
            var parser = new OperationFilesParser(GetParsersProvider(new List<Type>()));

            var result = await parser.ParseOperationsFiles(new string[] { windows1250FilePath });

            Assert.AreEqual(0, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(1, result.ParsingResults.Count);
            var fileImportResult = result.ParsingResults[0];
            Assert.IsTrue(fileImportResult.IsError);
            Assert.IsNull(fileImportResult.Exception);
        }

        [TestMethod]
        public async Task Should_ReturnIsErrorWithExceptionResult_When_FileDoesNotExist()
        {
            var parser = new OperationFilesParser(GetParsersProvider(new List<Type>()));

            var result = await parser.ParseOperationsFiles(new string[] { "not_existing_file.txt" });

            Assert.AreEqual(0, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(1, result.ParsingResults.Count);
            var fileImportResult = result.ParsingResults[0];
            Assert.IsTrue(fileImportResult.IsError);
            Assert.IsNotNull(fileImportResult.Exception);
        }

        [TestMethod]
        public async Task Should_ReadUft8FileCorrectly()
        {
            string fileContents = File.ReadAllText(utf8FilePath, Encoding.UTF8);

            var parser = new OperationFilesParser(GetParsersProvider(new List<Type>() { typeof(FakeAlwaysCanParseUtf8Parser) }));

            var result = await parser.ParseOperationsFiles(new string[] { utf8FilePath });

            Assert.AreEqual(1, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(1, result.ParsingResults.Count);
            var fileImportResult = result.ParsingResults[0];
            Assert.IsFalse(fileImportResult.IsError);
            Assert.AreEqual(fileContents, result.LoadedOperations[0].Description);
        }

        [TestMethod]
        public async Task Should_Read1250FileCorrectly()
        {
            string fileContents = File.ReadAllText(windows1250FilePath, CodePagesEncodingProvider.Instance.GetEncoding(1250));

            var parser = new OperationFilesParser(GetParsersProvider(new List<Type>() { typeof(FakeAlwaysCanParseWindows1250Parser) }));

            var result = await parser.ParseOperationsFiles(new string[] { windows1250FilePath });

            Assert.AreEqual(1, result.LoadedOperations.Count);
            Assert.AreEqual(0, result.PairableOperations.Count());
            Assert.AreEqual(1, result.ParsingResults.Count);
            var fileImportResult = result.ParsingResults[0];
            Assert.IsFalse(fileImportResult.IsError);
            Assert.AreEqual(fileContents, result.LoadedOperations[0].Description);
        }

        private IServiceProvider GetParsersProvider(List<Type> types)
        {
            var services = new ServiceCollection();
            foreach (var type in types)
                services.AddTransient(typeof(IOperationsFileParser), type);

            return services.BuildServiceProvider();
        }


        private string GetWindows1250FilePath()
        {
            string path = Path.GetTempFileName();
            WriteResourceToFile(path, "GnuCash.TransactionImportGenerator.Tests.TestData.OperationFilesParserTests_1250.txt");
            return path;
        }

        private string GetUtf8FilePath()
        {
            string path = Path.GetTempFileName();
            WriteResourceToFile(path, "GnuCash.TransactionImportGenerator.Tests.TestData.OperationFilesParserTests_utf8.txt");
            return path;
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
                //if (disposing)
                //{
                //    // dispose managed state (managed objects)
                //}

                File.Delete(windows1250FilePath);
                File.Delete(utf8FilePath);
                disposedValue = true;
            }
        }

        ~OperationFilesParserTests()
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
