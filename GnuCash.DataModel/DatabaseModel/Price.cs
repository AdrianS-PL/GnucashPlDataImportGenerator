using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class Price
    {
        [Key]
        public string guid { get; set; }
        public string commodity_guid { get; set; }
        public string currency_guid { get; set; }
        public DateTime date { get; set; }

        [ForeignKey(nameof(commodity_guid))]
        public virtual Commodity CommodityInstance { get; set; }
    }
}
