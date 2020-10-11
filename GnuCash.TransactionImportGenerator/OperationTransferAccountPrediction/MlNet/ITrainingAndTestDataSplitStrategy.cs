using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    public interface ITrainingAndTestDataSplitStrategy<PreSplitInputType>
    {
        (IDataView trainingDataView, IDataView testDataView) SplitTrainingAndTestData(IEnumerable<PreSplitInputType> data);
    }
}
