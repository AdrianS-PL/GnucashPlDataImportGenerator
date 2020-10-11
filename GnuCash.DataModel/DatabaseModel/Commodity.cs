using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class Commodity
    {
        [Key]
        public string guid { get; set; }

        [Column("namespace")]
        public string Namespace { get; set; }

        public string mnemonic { get; set; }
        
        public string fullname { get; set; }

        public string cusip { get; set; }

        [InverseProperty(nameof(Price.CommodityInstance))]
        public virtual ICollection<Price> Prices { get; set; }
    }
}
