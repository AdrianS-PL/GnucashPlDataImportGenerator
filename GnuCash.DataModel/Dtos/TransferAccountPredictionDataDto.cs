using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.DataModel.Dtos
{
    public class TransferAccountPredictionDataDto
    {
        public string Description { get; set; }
        public string TransferAccountGuid { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
