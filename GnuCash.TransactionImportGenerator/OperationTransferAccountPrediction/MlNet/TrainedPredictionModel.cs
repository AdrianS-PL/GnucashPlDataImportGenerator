using GnuCash.TransactionImportGenerator.Model;
using GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet.Model;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GnuCash.TransactionImportGenerator.OperationTransferAccountPrediction.MlNet
{
    class TrainedPredictionModel : ITrainedPredictionModel
    {
        private sealed class OperationPrediction
        {
            [ColumnName("PredictedLabel")]
            public string TransferAccountId { get; set; }

            public float[] Score { get; set; }
        }

        private readonly byte[] _trainedModelData;        

        public double MicroAccuracyPercent { get; }

        public double MacroAccuracyPercent { get; }

        public TrainedPredictionModel(byte[] trainedModelData, double microAccuracyPercent, double macroAccuracyPercent)
        {
            _trainedModelData = trainedModelData;
            MicroAccuracyPercent = microAccuracyPercent;
            MacroAccuracyPercent = macroAccuracyPercent;
        }

        public string PredictTransferAccount(Operation operation, string defaultTransferAccount, float cutoffScore = 0.5F)
        {
            var inputRow = new MlNetPredictionInputRow()
            {
                Description = operation.Description
            };

            MLContext ml = new MLContext();

            using MemoryStream ms = new MemoryStream(_trainedModelData);
            ITransformer trainedModel = ml.Model.Load(ms, out DataViewSchema inputSchema);

            var predEngine = ml.Model.CreatePredictionEngine<MlNetPredictionInputRow, OperationPrediction>(trainedModel);

            var prediction = predEngine.Predict(inputRow);

            float maxScore = prediction.Score.Max();

            if (maxScore > cutoffScore)
                return prediction.TransferAccountId;
            else
                return defaultTransferAccount;
        }
    }
}
