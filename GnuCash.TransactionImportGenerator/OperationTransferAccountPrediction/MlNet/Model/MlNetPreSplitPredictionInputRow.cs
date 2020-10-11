using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model
{
    class MlNetPreSplitPredictionInputRow
    {
        public MlNetPreSplitPredictionInputRow() { }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string TransferAccountId { get; set; }
    }
}
