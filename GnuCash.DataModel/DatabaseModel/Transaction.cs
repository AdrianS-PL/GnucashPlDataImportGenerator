using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class Transaction
    {
        [Key]
        public string guid { get; set; }
        
        public string currency_guid { get; set; }
        
        public string num { get; set; }
        
        public DateTime? post_date { get; set; }
        
        public DateTime? enter_date { get; set; }
        
        public string description { get; set; }

        [InverseProperty(nameof(Split.TransactionInstance))]
        public virtual ICollection<Split> Splits { get; set; }
    }
}
