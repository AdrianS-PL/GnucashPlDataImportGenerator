using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model
{
    class MlNetPredictionInputRow
    {
        public MlNetPredictionInputRow() { }

        public MlNetPredictionInputRow(string description, string transferAccountId)
        {
            Description = description;
            TransferAccountId = transferAccountId;
        }

        public string Description { get; set; }
        public string TransferAccountId { get; set; }
    }
}
