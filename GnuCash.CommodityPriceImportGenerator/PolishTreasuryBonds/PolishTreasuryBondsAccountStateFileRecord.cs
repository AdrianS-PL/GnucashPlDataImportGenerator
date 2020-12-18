using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.CommodityPriceImportGenerator.PolishTreasuryBonds
{
    internal class PolishTreasuryBondsAccountStateFileRecord
    {
        public string EmissionCode { get; set; }
        public int AvailableBonds { get; set; }
        public int BlockedBonds { get; set; }
        public decimal NominalValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime BuyoutDate { get; set; }
    }
}
