using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    class EmptyPredictionModel : ITrainedPredictionModel
    {
        public double MicroAccuracyPercent => 0;

        public double MacroAccuracyPercent => 0;

        public string PredictTransferAccount(Operation operation, string defaultTransferAccount, float cutoffScore = 0.5F)
        {
            return defaultTransferAccount;
        }
    }
}
