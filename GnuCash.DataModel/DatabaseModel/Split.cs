using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class Split
    {
        [Key]
        public string guid { get; set; }        
        
        public string tx_guid { get; set; }

        public string account_guid { get; set; }

        public string memo { get; set; }
        
        public string action { get; set; }
        
        public string reconcile_state { get; set; }

        public DateTime? reconcile_date { get; set; }

        public long value_num { get; set; }

        public long value_denom { get; set; }

        public long quantity_num { get; set; }
        
        public long quantity_denom { get; set; }
        
        public string? lot_guid { get; set; }

        [ForeignKey(nameof(tx_guid))]
        public virtual Transaction TransactionInstance { get; set; }

        [ForeignKey(nameof(account_guid))]
        public virtual Account AccountInstance { get; set; }
    }
}
