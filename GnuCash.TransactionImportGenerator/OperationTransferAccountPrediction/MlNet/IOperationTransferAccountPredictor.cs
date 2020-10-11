using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    public interface IOperationTransferAccountPredictor
    {
        Task<ITrainedPredictionModel> CreatePredictionModel();
    }
}
