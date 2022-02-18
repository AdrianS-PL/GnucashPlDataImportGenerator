using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    public class PolishTreasuryBondsAccountStateFilesParsingResult
    {
        public List<FileImportResult> ParsingResults { get; set; } = new List<FileImportResult>();
        public List<PolishTreasuryBondsAccountStateFileRecord> LoadedFileRecords { get; set; } = new List<PolishTreasuryBondsAccountStateFileRecord>();
    }
}
