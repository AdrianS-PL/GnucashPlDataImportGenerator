using GnuCash.TransactionImportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    public interface ITrainedPredictionModel
    {
        string PredictTransferAccount(Operation operation, string defaultTransferAccount, float cutoffScore = 0.5f);
        double MicroAccuracyPercent { get; }
        double MacroAccuracyPercent { get; }
    }
}
