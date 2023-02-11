using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    public class PolishTreasuryBondsAccountStateFilesParser
    {
        private readonly IServiceProvider _serviceProvider;

        public PolishTreasuryBondsAccountStateFilesParser(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<PolishTreasuryBondsAccountStateFilesParsingResult> ParseOperationsFiles(string[] filePathnames)
        {
            var result = new PolishTreasuryBondsAccountStateFilesParsingResult();
            filePathnames ??= Array.Empty<string>();

            foreach (string filePathname in filePathnames)
            {
                var importResult = await ParseOperationsFile(filePathname);
                result.ParsingResults.Add(importResult);
                if (!importResult.IsError)
                    result.LoadedFileRecords.AddRange(importResult.FileData);
            }
            return result;
        }

        private async Task<FileImportResult> ParseOperationsFile(string filePathname)
        {
            try
            {
                var parser = _serviceProvider.GetServices<IPolishTreasuryBondsAccountStateFileParser>().FirstOrDefault(q => q.IsExtensionProcessable(Path.GetExtension(filePathname)));

                if (parser == null)
                {
                    return new FileImportResult()
                    {
                        IsError = true,
                        Message = "Niewłaściwy format pliku",
                        Pathname = filePathname
                    };
                }

                var bondRecords = await parser.ParseFile(filePathname);

                return new FileImportResult()
                {
                    IsError = false,
                    Message = "OK",
                    Pathname = filePathname,
                    FileData = bondRecords
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
