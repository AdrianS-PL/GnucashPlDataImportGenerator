using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GnuCash.DataModel.DatabaseModel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Database names")]
    public class Account
    {
        [Key]
        public string guid { get; set; }
        
        public string name { get; set; }
        
        public string account_type { get; set; }






        public string parent_guid { get; set; }

        public string code { get; set; }
        
        public string description { get; set; }

        public bool? hidden { get; set; }

        public bool? placeholder { get; set; }

        [InverseProperty(nameof(Split.AccountInstance))]
        public virtual ICollection<Split> Splits { get; set; }

        [ForeignKey(nameof(parent_guid))]
        public virtual Account Parent { get; set; }
    }
}
