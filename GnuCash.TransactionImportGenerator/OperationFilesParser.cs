using CsvHelper;
using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.Parsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator
{
    public class OperationFilesParser
    {
        private readonly IServiceProvider _serviceProvider;

        public OperationFilesParser(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }

        public async Task<OperationFilesParsingResult> ParseOperationsFiles(string[] filePathnames)
        {
            var result = new OperationFilesParsingResult();
            filePathnames ??= new string[0];

            foreach (string filePathname in filePathnames)
            {
                var importResult = await ParseOperationsFile(filePathname);
                result.ParsingResults.Add(importResult);
                if (!importResult.IsError)
                    result.LoadedOperations.AddRange(importResult.FileData);
            }
            return result;
        }

        private async Task<FileImportResult> ParseOperationsFile(string filePathname)
        {
            try
            {
                byte[] fileBytes = await File.ReadAllBytesAsync(filePathname);

                var parser = _serviceProvider.GetServices<IOperationsFileParser>().FirstOrDefault(q => q.CanParse(q.FileEncoding.GetString(fileBytes)));

                if (parser == null)
                {
                    return new FileImportResult()
                    {
                        IsError = true,
                        Message = "Invalid file format",
                        Pathname = filePathname
                    };
                }

                var operationsFile = parser.MapToOperationsFile(parser.FileEncoding.GetString(fileBytes));

                return new FileImportResult()
                {
                    IsError = false,
                    Message = "OK",
                    Pathname = filePathname,
                    FileData = operationsFile
                };
            }
            catch (Exception e)
            {
                return new FileImportResult()
                {
                    IsError = true,
                    Message = e.Message,
                    Pathname = filePathname,
                    Exception = e
                };
            }
        }
    }
}
