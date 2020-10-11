using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.DataModel.Dtos
{
    public class LastPriceDateDto
    {
        public string Mnemonic { get; set; }

        public string Namespace { get; set; }

        public DateTime Date { get; set; }
    }
}
