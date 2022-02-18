using GnuCash.DataModel.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    public class FileImportResult : FileImportResultBase
    {        
        public IList<PolishTreasuryBondsAccountStateFileRecord> FileData { get; set; }
    }
}
